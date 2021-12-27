using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.IO
{
    public class Farspeaker
	{
		private readonly ILogger<Farspeaker> logger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IMessenger messenger;
		private readonly IIdentityProvider identityProvider;

		public event EventHandler<PingArgs> PingReceived;
		public event EventHandler<ClientArgs> PlayerConnected;
		public event EventHandler<ClientArgs> PlayerDisconnected;
		public event EventHandler<NetEventArgs> NetEventReceived;

		public Farspeaker (ILogger<Farspeaker> logger,
							ITransmittableFactory transmittableFactory,
							IMessenger messenger,
							IIdentityProvider identityProvider,
							IHandlerProvider handlerProvider)
		{
			this.logger = logger;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;
			this.identityProvider = identityProvider;

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerDisconnectedHandler.PlayerDisconnected += OnPlayerDisconnected;
			handlerProvider.NetEventHandler.NetEventReceived += OnNetEvent;
		}

		#region Input
		public void Ping ()
		{
			var pingArgs = new PingArgs
			{
				InitialTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				InitiatorGuid = identityProvider.Id.Guid
			};
			var transmission = transmittableFactory.Build(TransmissionType.Ping, pingArgs);

			messenger.QueueTransmission(transmission);
		}

		public void SendNetEvent (NetEventArgs e)
		{
			var transmission = transmittableFactory.Build(TransmissionType.NetEvent, e);

			messenger.QueueTransmission(transmission);
		}
		#endregion

		#region Output
		private void OnPlayerDisconnected (object sender, ClientArgs e)
		{
			PlayerDisconnected?.Invoke(this, e);
		}

		private void OnPlayerConnected (object sender, ClientArgs e)
		{
			PlayerConnected?.Invoke(this, e);
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			if (e.InitiatorGuid.Equals(identityProvider.Id.Guid))
			{
				e.Ping = DateTimeOffset.Now.ToUnixTimeMilliseconds() - e.InitialTimestamp;

				Console.WriteLine($"ping: {e.Ping} ms");

				PingReceived?.Invoke(this, e);
			}
		}

		private void OnNetEvent (object sender, NetEventArgs e)
		{
			NetEventReceived?.Invoke(this, e);
		}
		#endregion
	}
}
