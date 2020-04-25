using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Factory
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
			return new Transmission(messageType, payload);
		}
	}
}
