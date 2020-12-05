using Moongate.Events.Reactor.EventHandlers;
using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;
using System;
using System.Collections.Generic;

namespace Moongate.Events.Reactor
{
	public class EventReactor
	{
		private readonly ILogger logger;
		private readonly GameStateController gameStateController;
		private readonly PlayerStateController playerStateController;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IMessenger messenger;

		private readonly ISet<IEventHandler> eventHandlers;

		public EventReactor (ILogger logger, 
			IHandlerProvider handlerProvider, 
			GameStateController gameStateController, 
			PlayerStateController playerStateController,
			ITransmittableFactory transmittableFactory, 
			IMessageListener messageListener,
			IMessenger messenger,
			IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.gameStateController = gameStateController;
			this.playerStateController = playerStateController;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;

			eventHandlers = new HashSet<IEventHandler>
			{
				new MessageListenerEventHandler(messageListener, messenger, transmittableFactory, identityProvider)
			};

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerInputHandler.PlayerInput += OnPlayerInput;
			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerHandshakeHandler.PlayerHandshake += OnPlayerHandshake;
			handlerProvider.PlayerDisconnectedHandler.PlayerDisconnected += OnPlayerDisconnected;
			handlerProvider.EntityHandler.EntityReceived += OnEntityReceived;
			handlerProvider.GameStateRequestHandler.GameStateReceived += GameStateReceived;
		}


		private void GameStateReceived (object sender, GameStateRequestArgs e)
		{
			if (e.GameState != null)
			{ 
				gameStateController.GameState = e.GameState;
			}
		}

		private void OnEntityReceived (object sender, EntityArgs e)
		{
			gameStateController.ProcessEntity(e.Entity);
		}

		private void OnPlayerDisconnected (object sender, PlayerConnectionArgs e)
		{
			playerStateController.RemovePlayer(e.ConnectionId);
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs e)
		{
			playerStateController.AddOrUpdatePlayer(e.ConnectionId, e.Guid, e.Name);
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs e)
		{
			playerStateController.AddOrUpdatePlayer(e.ConnectionId, null, null);
		}

		private void OnPlayerInput (object sender, PlayerInputArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.PlayerInput, e);

			messenger.QueueTransmission(transmission);
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			if (e.Ping == null)
			{
				e.Ping = DateTimeOffset.Now.ToUnixTimeMilliseconds() - e.InitialTimestamp;

				var transmission = transmittableFactory.Build(TransmissionType.Ping, e);

				messenger.QueueTransmission(transmission);
			}
		}
	}
}
