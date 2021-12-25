using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class NetEventHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<NetEventArgs> NetEventReceived;

		public NetEventHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var args = (NetEventArgs)t.Payload;

			NetEventReceived?.Invoke(this, args);
		}
	}
}
