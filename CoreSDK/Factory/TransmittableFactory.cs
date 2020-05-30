using CoreNET.Controllers.Messenger;
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

		public ITransmittable Build (ConnectionId toId, MessageType messageType, object payload)
		{
			var t = new Transmission()
			{
				ToId = toId,
				MessageType = messageType,
				Payload = payload,
				SenderConnectionId = LocalId.ConnectionId,
				SenderGuid = LocalId.Guid
			};
			return t;
		}
	}
}
