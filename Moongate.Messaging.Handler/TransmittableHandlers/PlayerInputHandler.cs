using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerInputHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<PlayerInputArgs> PlayerInput;

		public PlayerInputHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var playerInputArgs = (PlayerInputArgs)t.Payload;

			PlayerInput?.Invoke(this, playerInputArgs);
		}
	}
}
