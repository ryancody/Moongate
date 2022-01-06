using Microsoft.Extensions.Logging;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.Events
{
    public class ServerEventHandler : IEventHandler
	{
		private readonly ILogger<ServerEventHandler> logger;
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly PlayerStateController playerStateController;

		public ServerEventHandler (
			ILogger<ServerEventHandler> logger,
			IHandlerProvider handlerProvider, 
			IMessageListener messageListener,
			IMessenger messenger, 
			ITransmittableFactory transmittableFactory,
			PlayerStateController playerStateController)
		{
			this.logger = logger;
			this.messenger = messenger;
			this.transmittableFactory = transmittableFactory;
			this.playerStateController = playerStateController;

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerHandshakeHandler.PlayerHandshake += OnPlayerHandshake;
			handlerProvider.NetEventHandler.NetEventReceived += OnNetEvent;
			messageListener.Connected += MessageListener_Connected;
			messageListener.Disconnected += MessageListener_Disconnected;
		}

		/// <summary>
		/// On receipt of a player handshake message.
		/// Server notifies other clients of new client's info
		/// with a player connected message. 
		/// Only server should receieve player handshake event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="newPlayer"></param>
		private void OnPlayerHandshake (object sender, ClientArgs newPlayer)
		{
			playerStateController.AddOrUpdatePlayer(newPlayer.ConnectionId, newPlayer.Guid, newPlayer.Name);

			playerStateController.GetPlayers().ForEach(p =>
			{
				var player = new ClientArgs
				{
					ConnectionId = p.ConnectionId,
					Guid = p.Guid,
					Name = p.Name
				};

				var playerStateUpdate = transmittableFactory.Build(TransmissionType.PlayerConnected, player);
				messenger.QueueTransmission(playerStateUpdate);
			});

			var transmission = transmittableFactory.Build(TransmissionType.PlayerConnected, newPlayer);
			messenger.QueueTransmission(transmission);

			Console.WriteLine($"player connected: {newPlayer.Name} - {newPlayer.Guid}");
			logger.LogInformation($"player connected: {newPlayer.Name} - {newPlayer.Guid}");
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.Ping, e);

			messenger.QueueTransmission(transmission);
		}

		private void OnNetEvent (object sender, NetEventArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.NetEvent, e);

			messenger.QueueTransmission(transmission);
		}

		private void MessageListener_Connected(object sender, MessageArgs e)
		{
			Console.WriteLine($"player [connection id {e.FromConnectionId}] connected, initiating handshake");
			logger.LogInformation($"player [connection id {e.FromConnectionId}] connected, initiating handshake");
		}

		private void MessageListener_Disconnected(object sender, MessageArgs e)
		{
			Console.WriteLine($"player [connection id {e.FromConnectionId}] disconnected");
			logger.LogInformation($"player [connection id {e.FromConnectionId}] disconnected");


			var disconnectedClient = new ClientArgs
			{
				ConnectionId = e.FromConnectionId,
				Guid = playerStateController.GetPlayer(e.FromConnectionId).Guid,
				Name = playerStateController.GetPlayer(e.FromConnectionId).Name
			};

			playerStateController.RemovePlayer(e.FromConnectionId);

			var playerStateUpdate = transmittableFactory.Build(TransmissionType.PlayerDisconnected, disconnectedClient);
			messenger.QueueTransmission(playerStateUpdate);
		}
	}
}
