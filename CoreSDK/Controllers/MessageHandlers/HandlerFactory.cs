using CoreSDK.Controllers;
using CoreSDK.Factory;
using System;
using System.Collections.Generic;

namespace CoreSDK
{
	class HandlerFactory : IHandlerFactory
	{
		readonly Dictionary<MessageType, IMessageHandler> handlers;
		readonly ILogger logger;

		public HandlerFactory (ILogger l)
		{
			logger = l;

			handlers = new Dictionary<MessageType, IMessageHandler>
			{
				{ MessageType.PlayerConnected, new PlayerConnectedHandler(logger) },
				{ MessageType.PlayerHandshake, new PlayerHandshakeHandler(logger) },
				{ MessageType.PlayerInput, new PlayerInputHandler(logger) },

				{ MessageType.GameStateRequest, new GameStateRequestHandler(logger) },
				{ MessageType.EntityTransmit, new EntityHandler(logger) },

				{ MessageType.Ping, new PingHandler(logger) }
			};
		}

		public IMessageHandler GetHandler (MessageType t)
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
