using Moongate.Network.Events;
using Network.Controllers;
using Network.Models;
using System;

namespace Network
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
				ConnectionId = message.SenderConnectionId.GetValueOrDefault()
			};

			PlayerConnected?.Invoke(this, playerConnectionArgs);
		}
	}
}
