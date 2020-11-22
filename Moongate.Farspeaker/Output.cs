using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Models.Events;
using System;

namespace Moongate.Farspeaker
{
	public class Output
	{
		private readonly ILogger logger;

		public event EventHandler<PingArgs> PingReceived;
		public event EventHandler<PlayerInputArgs> PlayerInput;
		public event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public event EventHandler<PlayerConnectionArgs> PlayerDisconnected;
		public event EventHandler<EntityArgs> EntityReceived;
		public event EventHandler<GameStateRequestArgs> GameStateReceived;

		public Output (ILogger logger, IHandlerProvider handlerProvider)
		{
			this.logger = logger;

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerInputHandler.PlayerInput += OnPlayerInput;
			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerDisconnectedHandler.PlayerDisconnected += OnPlayerDisconnected;
			handlerProvider.EntityHandler.EntityReceived += OnEntityReceived;
			handlerProvider.GameStateRequestHandler.GameStateReceived += GameStateReceived;
		}

		private void OnEntityReceived (object sender, EntityArgs e)
		{
			EntityReceived?.Invoke(this, e);
		}

		private void OnPlayerDisconnected (object sender, PlayerConnectionArgs e)
		{
			PlayerDisconnected?.Invoke(this, e);
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs e)
		{
			PlayerConnected?.Invoke(this, e);
		}

		private void OnPlayerInput (object sender, PlayerInputArgs e)
		{
			PlayerInput?.Invoke(this, e);
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			PingReceived?.Invoke(this, e);
		}

	}
}
