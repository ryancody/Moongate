using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerInputHandler : BaseHandler, IMessageHandler
	{
		public event EventHandler<ControlArgs> PlayerInputChanged;

		public PlayerInputHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable message)
		{
			var playerInputArgs = (ControlArgs)message.Payload;

			PlayerInputChanged?.Invoke(this, playerInputArgs);
		}
	}
}
