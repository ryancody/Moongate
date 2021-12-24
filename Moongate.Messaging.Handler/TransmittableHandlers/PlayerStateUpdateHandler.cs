using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerStateUpdateHandler : BaseHandler, ITransmissionHandler
	{
		public static event EventHandler<PlayerStateArgs> PlayerStateUpdated;

		public PlayerStateUpdateHandler () : base() { }

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
