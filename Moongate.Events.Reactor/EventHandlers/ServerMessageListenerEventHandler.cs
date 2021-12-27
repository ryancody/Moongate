using Microsoft.Extensions.Logging;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.Events.Reactor.EventHandlers
{
	public class ServerMessageListenerEventHandler : IMessageListenerEventHandler
	{
		private readonly ILogger<ServerMessageListenerEventHandler> logger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IMessenger messenger;
		private readonly PlayerStateController playerStateController;

		public ServerMessageListenerEventHandler (ILogger<ServerMessageListenerEventHandler> logger,
			IMessageListener messageListener,
			ITransmittableFactory transmittableFactory,
			IMessenger messenger,
			PlayerStateController playerStateController)
		{
			this.logger = logger;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;
			this.playerStateController = playerStateController;

			messageListener.Connected += MessageListener_Connected;
			messageListener.Disconnected += MessageListener_Disconnected;
		}

		private void MessageListener_Connected (object sender, MessageArgs e)
		{
			Console.WriteLine($"player [connection id {e.FromConnectionId}] connected, initiating handshake");
			logger.LogInformation($"player [connection id {e.FromConnectionId}] connected, initiating handshake");
		}

		private void MessageListener_Disconnected (object sender, MessageArgs e)
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
