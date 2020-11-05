using Moongate.Logger;
using Moongate.Transmittable.Models;
using System;
using System.Collections.Generic;

namespace Moongate.Messaging.Handler
{
	class HandlerFactory : IHandlerFactory
	{
		readonly Dictionary<TransmissionType, IMessageHandler> handlers;

		public HandlerFactory (ILogger logger)
		{
			handlers = new Dictionary<TransmissionType, IMessageHandler>
			{
				{ TransmissionType.PlayerConnected, new PlayerConnectedHandler(logger) },
				{ TransmissionType.PlayerHandshake, new PlayerHandshakeHandler(logger) },
				{ TransmissionType.PlayerInput, new PlayerInputHandler(logger) },

				{ TransmissionType.GameStateRequest, new GameStateRequestHandler(logger) },
				{ TransmissionType.EntityTransmit, new EntityHandler(logger) },

				{ TransmissionType.Ping, new PingHandler(logger) }
			};
		}

		public IMessageHandler GetHandler (TransmissionType t)
		{
			try
			{
				return handlers[t];
			}
			catch
			{
				throw new Exception("Message type not found in Handler Factory: " + t);
			}
		}
	}
}
