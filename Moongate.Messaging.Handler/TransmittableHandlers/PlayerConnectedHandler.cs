using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerConnectedHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<ClientArgs> PlayerConnected;

		public PlayerConnectedHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var clientArgs = (ClientArgs)t.Payload;

			PlayerConnected?.Invoke(this, clientArgs);
		}
	}
}
