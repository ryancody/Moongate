using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerHandshakeHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<PlayerHandshakeArgs> PlayerHandshake;

		public PlayerHandshakeHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			// unpack payload
			var playerHandshakeArgs = (PlayerHandshakeArgs)t.Payload;
			playerHandshakeArgs.ConnectionId = t.SenderConnectionId.GetValueOrDefault();

			logger.Debug($@"PlayerHandshakeHandler: 
			 - {playerHandshakeArgs.Name}");

			PlayerHandshake?.Invoke(this, playerHandshakeArgs);
		}
	}
}