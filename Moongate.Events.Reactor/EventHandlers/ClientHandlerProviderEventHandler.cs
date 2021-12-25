using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Models.Events;
using Moongate.State.Controller;
using System;

namespace Moongate.Events.Reactor.EventHandlers
{
	public class ClientHandlerProviderEventHandler : IEventHandler
	{
		private readonly ILogger<ClientHandlerProviderEventHandler> logger;
		private readonly IIdentityProvider identityProvider;
		private readonly PlayerStateController playerStateController;

		public ClientHandlerProviderEventHandler (ILogger<ClientHandlerProviderEventHandler> logger, 
			IHandlerProvider handlerProvider, 
			IIdentityProvider identityProvider,
			PlayerStateController playerStateController)
		{
			this.logger = logger;
			this.identityProvider = identityProvider;
			this.playerStateController = playerStateController;

			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerDisconnectedHandler.PlayerDisconnected += OnPlayerDisconnected;
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
	}
}
