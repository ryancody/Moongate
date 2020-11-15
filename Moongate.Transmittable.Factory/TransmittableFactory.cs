using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Models.Transmittable;

namespace Moongate.Transmittable.Factory
{
	class TransmittableFactory : ITransmittableFactory
	{
		private readonly ILogger logger;
		private readonly IIdentityProvider identityProvider;

		public TransmittableFactory (ILogger _logger, IIdentityProvider identityProvider)
		{
			logger = _logger;
			this.identityProvider = identityProvider;
		}

		public ITransmittable Build (TransmissionType messageType, object payload)
		{
			var t = new Transmission()
			{
				TransmissionType = messageType,
				Payload = payload,
				SenderGuid = identityProvider.Id.Guid
			};
			return t;
		}
	}
}
