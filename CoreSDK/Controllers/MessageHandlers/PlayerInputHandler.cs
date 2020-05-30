using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class PlayerInputHandler : IMessageHandler
	{
		readonly ILogger logger;

		public event EventHandler<PlayerInputArgs> PlayerInput;

		public PlayerInputHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (ITransmittable message)
		{
			var playerInputArgs = (PlayerInputArgs)message.Payload;

			PlayerInput?.Invoke(this, playerInputArgs);
		}
	}
}
