using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class GameStateRequestHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<GameStateRequestArgs> GameStateReceived;
		public event EventHandler<GameStateRequestArgs> GameStateRequested;

		public GameStateRequestHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			if (t.Payload == null)
			{
				logger.Debug("received gamestate request");

				var requestArgs = new GameStateRequestArgs()
				{
					SenderGuid = t.SenderGuid
				};

				GameStateRequested?.Invoke(this, requestArgs);
			}
			else
			{
				logger.Debug("received gamestate");

				var gameStateReceievedArgs = (GameStateRequestArgs)t.Payload;

				GameStateReceived?.Invoke(this, gameStateReceievedArgs);
			}
		}
	}
}