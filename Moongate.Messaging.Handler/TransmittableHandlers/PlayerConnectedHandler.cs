using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerConnectedHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<PlayerConnectionArgs> PlayerConnected;

		public PlayerConnectedHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var playerConnectionArgs = new PlayerConnectionArgs()
			{
				ConnectionId = t.SenderConnectionId.GetValueOrDefault()
			};

			PlayerConnected?.Invoke(this, playerConnectionArgs);
		}
	}
}
