using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerInputHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<PlayerInputArgs> PlayerInput;

		public PlayerInputHandler () : base() { }

		public void Handle (ITransmittable t)
		{
			var playerInputArgs = (PlayerInputArgs)t.Payload;

			PlayerInput?.Invoke(this, playerInputArgs);
		}
	}
}
