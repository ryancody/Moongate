using Network.Controllers;
using Network.Models;
using System;

namespace Network
{
	public class PlayerHandshakeHandler : IMessageHandler
	{
		readonly ILogger logger;

		public event EventHandler<PlayerHandshakeArgs> PlayerHandshake;

		public PlayerHandshakeHandler (ILogger l)
		{
			logger = l;
		}

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