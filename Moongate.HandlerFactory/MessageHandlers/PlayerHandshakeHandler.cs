using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.HandlerFactory.MessageHandlers
{
	public class PlayerHandshakeHandler : BaseHandler, IMessageHandler
	{
		public event EventHandler<PlayerHandshakeArgs> PlayerHandshake;

		public PlayerHandshakeHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable m)
		{
			// unpack payload
			var playerHandshakeArgs = (PlayerHandshakeArgs)m.Payload;
			playerHandshakeArgs.ConnectionId = m.SenderConnectionId.GetValueOrDefault();

			logger.Debug($@"PlayerHandshakeHandler: 
			 - {playerHandshakeArgs.Name}");

			PlayerHandshake?.Invoke(this, playerHandshakeArgs);
		}

		public void Sub (Delegate listener)
		{
			PlayerHandshake += (EventHandler<PlayerHandshakeArgs>)listener;
		}
	}
}