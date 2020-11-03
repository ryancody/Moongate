using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.State.Models;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.HandlerFactory.MessageHandlers
{
	public class PlayerStateUpdateHandler : BaseHandler, IMessageHandler
	{
		public static event EventHandler<PlayerStateArgs> PlayerStateUpdated;

		public PlayerStateUpdateHandler (ILogger logger) : base(logger) { }

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
