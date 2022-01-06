using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
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
	public class ClientEventHandler : IEventHandler
	{
		private readonly ILogger<ClientEventHandler> logger;
		private readonly IIdentityProvider identityProvider;
		private readonly PlayerStateController playerStateController;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IMessenger messenger;

		public ClientEventHandler (ILogger<ClientEventHandler> logger, 
			IHandlerProvider handlerProvider, 
			ITransmittableFactory transmittableFactory,
			IMessageListener messageListener,
			IIdentityProvider identityProvider,
			PlayerStateController playerStateController,
			IMessenger messenger)
		{
			this.logger = logger;
			this.identityProvider = identityProvider;
			this.playerStateController = playerStateController;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;

			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerDisconnectedHandler.PlayerDisconnected += OnPlayerDisconnected;
			messageListener.Connected += MessageListener_Connected;
			messageListener.Disconnected += MessageListener_Disconnected;
		}

		private void OnPlayerDisconnected (object sender, ClientArgs e)
		{
			Console.WriteLine($"player disconnected: {e.Name} - {e.Guid}");
			logger.LogDebug($"player disconnected: {e.Name} - {e.Guid}");

			playerStateController.RemovePlayer(e.ConnectionId);
		}

		/// <summary>
		/// On receipt of a player connected message.
		/// Indicates a player has completed the handshake process
		/// and server has notified other clients of the connection.
		/// Only clients should receive Player Connected.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPlayerConnected (object sender, ClientArgs e)
		{
			Console.WriteLine($"player connected: {e.Name} - {e.Guid}");
			logger.LogInformation($"player connected: {e.Name} - {e.Guid}");

			if (e.Guid.Equals(identityProvider.Id.Guid))
			{
				identityProvider.Id.ConnectionId = e.ConnectionId;
			}

			playerStateController.AddOrUpdatePlayer(e.ConnectionId, e.Guid, e.Name);
		}

		private void MessageListener_Connected(object sender, MessageArgs e)
		{
			Console.WriteLine($"connected to server");
			logger.LogInformation($"connected to server");

			var playerHandshakeArgs = new ClientArgs
			{
				Name = identityProvider.Id.Name,
				Guid = identityProvider.Id.Guid
			};
			var transmission = transmittableFactory.Build(TransmissionType.PlayerHandshake, playerHandshakeArgs);

			messenger.QueueTransmission(transmission);
		}

		private void MessageListener_Disconnected(object sender, MessageArgs e)
		{
			Console.WriteLine("disconnected from server");
			logger.LogInformation("disconnected from server");
		}
	}
}
