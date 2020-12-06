using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.Events.Reactor.EventHandlers
{
	public class ServerHandlerProviderEventHandler : IEventHandler
	{
		private readonly ILogger logger;
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IIdentityProvider identityProvider;
		private readonly GameStateController gameStateController;
		private readonly PlayerStateController playerStateController;

		public ServerHandlerProviderEventHandler (ILogger logger, 
			IHandlerProvider handlerProvider, 
			IMessenger messenger, 
			ITransmittableFactory transmittableFactory, 
			IIdentityProvider identityProvider,
			GameStateController gameStateController,
			PlayerStateController playerStateController)
		{
			this.logger = logger;
			this.messenger = messenger;
			this.transmittableFactory = transmittableFactory;
			this.identityProvider = identityProvider;
			this.gameStateController = gameStateController;
			this.playerStateController = playerStateController;

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerInputHandler.PlayerInput += OnPlayerInput;
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

		private void OnPlayerDisconnected (object sender, ClientArgs e)
		{
			playerStateController.RemovePlayer(e.ConnectionId);
		}

		/// <summary>
		/// On receipt of a player handshake message.
		/// Server notifies other clients of new client's info
		/// with a player connected message. 
		/// Only server should receieve player handshake event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnPlayerHandshake (object sender, ClientArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.PlayerConnected, e);

			messenger.QueueTransmission(transmission);

			playerStateController.AddOrUpdatePlayer(e.ConnectionId, e.Guid, e.Name);
		}

		private void OnPlayerInput (object sender, PlayerInputArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.PlayerInput, e);

			messenger.QueueTransmission(transmission);
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.Ping, e);

			messenger.QueueTransmission(transmission);
		}
	}
}
