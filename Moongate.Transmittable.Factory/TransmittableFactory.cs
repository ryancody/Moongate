using Moongate.Identity.Provider;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;

namespace Moongate.Transmittable.Factory
{
	public class TransmittableFactory : ITransmittableFactory
	{
		private readonly IIdentityProvider identityProvider;

		public TransmittableFactory (IIdentityProvider identityProvider)
		{
			this.identityProvider = identityProvider;
		}

		public ITransmittable Build (TransmissionType messageType, IEventArgs payload)
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
