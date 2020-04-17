using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class PlayerHandshakeHandler : IMessageHandler
	{
		public static event EventHandler<PlayerHandshakeArgs> PlayerHandshaked;

		readonly ILogger logger;

		public PlayerHandshakeHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (Transmission m)
		{
			// unpack payload
			var playerHandshakeArgs = (PlayerHandshakeArgs)m.Payload;

			// add ConnectionId to Player
			playerHandshakeArgs.ConnectionId = m.SenderConnectionId;

			logger.Debug($@"PlayerHandshakeHandler: 
			 - {playerHandshakeArgs.Name}
			 - {playerHandshakeArgs.ConnectionId}
			 - {playerHandshakeArgs.Guid}");

			PlayerHandshaked?.Invoke(this, playerHandshakeArgs);
		}
	}
}