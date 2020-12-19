using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.IO
{
	public class Input
	{
		private readonly ILogger logger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IMessenger messenger;
		private readonly IIdentityProvider identityProvider;

		internal Input (ILogger logger, ITransmittableFactory transmittableFactory, IMessenger messenger, IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;
			this.identityProvider = identityProvider;
		}

		public void Ping ()
		{
			var pingArgs = new PingArgs
			{
				InitialTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
				InitiatorGuid = identityProvider.Id.Guid
			};
			var transmission = transmittableFactory.Build(null, TransmissionType.Ping, pingArgs);

			messenger.QueueTransmission(transmission);
		}

		public void SendNetEvent (NetEventArgs e)
		{
			var transmission = transmittableFactory.Build(null, TransmissionType.NetEvent, e);

			messenger.QueueTransmission(transmission);
		}
	}
}
