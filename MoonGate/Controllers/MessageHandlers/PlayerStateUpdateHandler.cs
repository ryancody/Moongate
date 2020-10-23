using Moongate.Network.Events;
using Network.Controllers;
using Network.Models;
using System;

namespace Network
{
	public class PlayerStateUpdateHandler : IMessageHandler
	{
		readonly ILogger logger;

		public static event EventHandler<PlayerStateArgs> PlayerStateUpdated;
		
		public PlayerStateUpdateHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (ITransmittable message)
		{
			var playerStateArgs = new PlayerStateArgs()
			{
				PlayerState = (PlayerState)message.Payload
			};

			PlayerStateUpdated?.Invoke(this, playerStateArgs);
		}
	}
}
