using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class GameStateRequestHandler : BaseHandler, IMessageHandler
	{
		public static event EventHandler<GameStateRequestArgs> GameStateReceived;
		public static event EventHandler<GameStateRequestArgs> GameStateRequested;

		public GameStateRequestHandler (ILogger logger) : base(logger) { }

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