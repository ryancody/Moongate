using Network.Controllers;
using Network.Models;
using System;

namespace Network
{
	public class GameStateRequestHandler : IMessageHandler
	{
		readonly ILogger logger;

		public static event EventHandler<GameStateRequestArgs> GameStateReceived;
		public static event EventHandler<GameStateRequestArgs> GameStateRequested;

		public GameStateRequestHandler (ILogger logger)
		{
			this.logger = logger;
		}

		public void Handle (ITransmittable m)
		{
			if (m.Payload == null)
			{
				logger.Debug("received gamestate request");

				var requestArgs = new GameStateRequestArgs()
				{
					SenderGuid = m.SenderGuid
				};

				GameStateRequested?.Invoke(this, requestArgs);
			}
			else
			{
				logger.Debug("received gamestate");

				var gameStateReceievedArgs = (GameStateRequestArgs)m.Payload;

				GameStateReceived?.Invoke(this, gameStateReceievedArgs);
			}
		}

		public void Sub (Delegate listener)
		{
			throw new NotImplementedException();
		}
	}
}