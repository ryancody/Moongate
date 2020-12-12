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
	public class ClientHandlerProviderEventHandler : IEventHandler
	{
		private readonly ILogger logger;
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IIdentityProvider identityProvider;
		private readonly GameStateController gameStateController;
		private readonly PlayerStateController playerStateController;

		public ClientHandlerProviderEventHandler (ILogger logger, 
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

			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerDisconnectedHandler.PlayerDisconnected += OnPlayerDisconnected;
			handlerProvider.EntityHandler.EntityReceived += OnEntityReceived;
			handlerProvider.GameStateRequestHandler.GameStateReceived += OnGameStateReceived;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnGameStateReceived (object sender, GameStateRequestArgs e)
		{
			if (e.GameState != null)
			{
				gameStateController.GameState = e.GameState;
			}
		}

		private void OnEntityReceived (object sender, EntityArgs e)
		{
			Console.WriteLine("Received entity, processing");
			logger.Debug("Received entity, processing");

			gameStateController.ProcessEntity(e.Entity);
		}

		private void OnPlayerDisconnected (object sender, ClientArgs e)
		{
			Console.WriteLine($"player disconnected: {e.Name} - {e.Guid}");
			logger.Info($"player disconnected: {e.Name} - {e.Guid}");

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
			logger.Info($"player connected: {e.Name} - {e.Guid}");

			if (e.Guid.Equals(identityProvider.Id.Guid))
			{
				identityProvider.Id.ConnectionId = e.ConnectionId;
			}

			playerStateController.AddOrUpdatePlayer(e.ConnectionId, e.Guid, e.Name);
		}
	}
}
