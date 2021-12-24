using Microsoft.Extensions.Logging;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class GameStateRequestHandler : BaseHandler, ITransmissionHandler
	{
		private readonly ILogger<GameStateRequestHandler> logger;

		public event EventHandler<GameStateRequestArgs> GameStateReceived;
		public event EventHandler<GameStateRequestArgs> GameStateRequested;

		public GameStateRequestHandler (ILogger<GameStateRequestHandler> logger) : base() 
		{
			this.logger = logger;
		}

		public void Handle (ITransmittable t)
		{
			if (t.Payload == null)
			{
				logger.LogDebug("received gamestate request");

				var requestArgs = new GameStateRequestArgs()
				{
					SenderGuid = t.SenderGuid
				};

				GameStateRequested?.Invoke(this, requestArgs);
			}
			else
			{
				logger.LogDebug("received gamestate");

				var gameStateReceievedArgs = (GameStateRequestArgs)t.Payload;

				GameStateReceived?.Invoke(this, gameStateReceievedArgs);
			}
		}
	}
}