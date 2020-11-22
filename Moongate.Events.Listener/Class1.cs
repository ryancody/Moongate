using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Models.Events;
using System;

namespace Moongate.Events.Listener
{
	public class EventListener
	{
		private readonly ILogger logger;
		private readonly IHandlerProvider handlerProvider;
		public EventListener (ILogger logger)
		{

			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerHandshakeHandler.PlayerHandshake += OnPlayerHandshake;
			handlerProvider.PlayerDisconnectedHandler.OnPlayerDisconnected += OnPlayerDisconnected;
			handlerProvider.PlayerInputHandler.PlayerInput += OnPlayerInput;
			handlerProvider.GameStateRequestHandler.GameStateRequested += OnGameStateRequested;
			handlerProvider.EntityHandler.EntityReceived += OnEntityReceived;
			handlerProvider.PingHandler.PingReceived += OnPingReceived;
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs e)
		{
			throw new NotImplementedException();
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
