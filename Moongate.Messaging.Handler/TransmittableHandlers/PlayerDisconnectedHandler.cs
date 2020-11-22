using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerDisconnectedHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public PlayerDisconnectedHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var playerConnectionArgs = new PlayerConnectionArgs()
			{
				ConnectionId = t.SenderConnectionId.GetValueOrDefault()
			};

			PlayerDisconnected?.Invoke(this, playerConnectionArgs);
		}
	}
}
