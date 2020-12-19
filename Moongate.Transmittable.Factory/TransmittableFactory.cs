using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Transmittable.Factory
{
	public class TransmittableFactory : ITransmittableFactory
	{
		private readonly ILogger logger;
		private readonly IIdentityProvider identityProvider;

		public TransmittableFactory (ILogger _logger, IIdentityProvider identityProvider)
		{
			logger = _logger;
			this.identityProvider = identityProvider;
		}

		public ITransmittable Build (int? toConnectionId, TransmissionType messageType, IEventArgs payload)
		{
			var t = new Transmission()
			{
				ToConnectionId = toConnectionId,
				TransmissionType = messageType,
				Payload = payload,
				SenderGuid = identityProvider.Id.Guid
			};

			return t;
		}
	}
}
