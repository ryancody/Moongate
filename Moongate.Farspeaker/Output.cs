using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Models.Events;
using System;

namespace Moongate.IO
{
	public class Output
	{
		private readonly ILogger<Output> logger;
		private readonly IIdentityProvider identity;

		public event EventHandler<PingArgs> PingReceived;
		public event EventHandler<PlayerInputArgs> PlayerInput;
		public event EventHandler<ClientArgs> PlayerConnected;
		public event EventHandler<ClientArgs> PlayerDisconnected;
		public event EventHandler<EntityArgs> EntityReceived;
		public event EventHandler<GameStateRequestArgs> GameStateReceived;

		public Output (ILogger<Output> logger, IHandlerProvider handlerProvider, IIdentityProvider identity)
		{
			this.logger = logger;
			this.identity = identity;

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

		private void OnPlayerDisconnected (object sender, ClientArgs e)
		{
			PlayerDisconnected?.Invoke(this, e);
		}

		private void OnPlayerConnected (object sender, ClientArgs e)
		{
			PlayerConnected?.Invoke(this, e);
		}

		private void OnPlayerInput (object sender, PlayerInputArgs e)
		{
			PlayerInput?.Invoke(this, e);
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			if (e.InitiatorGuid.Equals(identity.Id.Guid))
			{ 
				e.Ping = DateTimeOffset.Now.ToUnixTimeMilliseconds() - e.InitialTimestamp;

				Console.WriteLine($"ping: {e.Ping} ms");

				PingReceived?.Invoke(this, e);
			}
		}
	}
}
