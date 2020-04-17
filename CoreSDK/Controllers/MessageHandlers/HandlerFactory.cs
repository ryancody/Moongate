using CoreSDK.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	class HandlerFactory
	{
		readonly Dictionary<MessageType, IMessageHandler> handlers;
		readonly ILogger logger;

		public HandlerFactory (ILogger l)
		{
			logger = l;

			handlers = new Dictionary<MessageType, IMessageHandler>
			{
				{ MessageType.PlayerHandshake, new PlayerHandshakeHandler(logger) },

				{ MessageType.GameStateRequest, new GameStateRequestHandler(logger) },
				{ MessageType.CreateEntity, new EntityHandler(logger) },
				{ MessageType.UpdateEntity, new UpdateEntityHandler() },
				{ MessageType.RequestPlayersList, new RequestPlayersListHandler() },

				{ MessageType.Ping, new PingHandler(logger) },
				{ MessageType.Pong, new PongHandler(logger) }
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
