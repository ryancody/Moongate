using Network.Controllers;
using Network.Models;
using System;

namespace Network
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
