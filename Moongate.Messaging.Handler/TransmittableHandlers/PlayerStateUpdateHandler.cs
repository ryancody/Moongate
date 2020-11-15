using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.State.Models;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerStateUpdateHandler : BaseHandler, ITransmissionHandler
	{
		public static event EventHandler<PlayerStateArgs> PlayerStateUpdated;

		public PlayerStateUpdateHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var playerStateArgs = new PlayerStateArgs()
			{
				PlayerState = (PlayerState)t.Payload
			};

			PlayerStateUpdated?.Invoke(this, playerStateArgs);
		}
	}
}
