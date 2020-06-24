using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class PlayerInputHandler : IMessageHandler
	{
		readonly ILogger logger;

		public event EventHandler<ControlArgs> PlayerInputChanged;

		public PlayerInputHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (ITransmittable message)
		{
			var playerInputArgs = (ControlArgs)message.Payload;

			PlayerInputChanged?.Invoke(this, playerInputArgs);
		}
	}
}
