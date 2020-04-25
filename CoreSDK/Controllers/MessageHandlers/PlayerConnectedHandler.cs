using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class PlayerConnectedHandler : IMessageHandler
	{
		readonly ILogger logger;

		public event EventHandler<PlayerConnectionArgs> PlayerConnected;

		public PlayerConnectedHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (ITransmittable message)
		{
			var playerConnectionArgs = new PlayerConnectionArgs()
			{
				 ConnectionId = message.SenderConnectionId
			};

			PlayerConnected?.Invoke(this, playerConnectionArgs);
		}
	}
}
