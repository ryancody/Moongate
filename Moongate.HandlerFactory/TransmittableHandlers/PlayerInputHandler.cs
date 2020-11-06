using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerInputHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<ControlArgs> PlayerInputChanged;

		public PlayerInputHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var playerInputArgs = (ControlArgs)t.Payload;

			PlayerInputChanged?.Invoke(this, playerInputArgs);
		}
	}
}
