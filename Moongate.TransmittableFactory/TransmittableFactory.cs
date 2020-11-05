using Moongate.Logger;
using Moongate.Models;
using Moongate.Transmittable.Models;

namespace Moongate.TransmittableFactory
{
	class TransmittableFactory : ITransmittableFactory
	{
		ILogger logger;

		public TransmittableFactory (ILogger _logger)
		{
			logger = _logger;
		}

		public ITransmittable Build (TransmissionType messageType, object payload)
		{
			var t = new Transmission()
			{
				TransmissionType = messageType,
				Payload = payload,
				SenderGuid = LocalId.Guid
			};
			return t;
		}
	}
}
