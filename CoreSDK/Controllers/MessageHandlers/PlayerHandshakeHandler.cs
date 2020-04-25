using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
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

			// add ConnectionId to Player
			playerHandshakeArgs.ConnectionId = m.SenderConnectionId;

			logger.Debug($@"PlayerHandshakeHandler: 
			 - {playerHandshakeArgs.Name}
			 - {playerHandshakeArgs.ConnectionId}
			 - {playerHandshakeArgs.Guid}");

			PlayerHandshake?.Invoke(this, playerHandshakeArgs);
		}

		public void Sub (Delegate listener)
		{
			PlayerHandshake += (EventHandler<PlayerHandshakeArgs>)listener;
		}
	}
}