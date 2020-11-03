﻿using Moongate.Logger;
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

		public ITransmittable Build (MessageType messageType, object payload)
		{
			var t = new Transmission()
			{
				MessageType = messageType,
				Payload = payload,
				SenderGuid = LocalId.Guid
			};
			return t;
		}
	}
}
