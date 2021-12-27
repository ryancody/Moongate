using Microsoft.Extensions.Logging;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
    public class NetEventHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<NetEventArgs> NetEventReceived;

		public NetEventHandler (ILogger<NetEventHandler> logger) : base() { }

		public void Handle (ITransmittable t)
		{
			var args = (NetEventArgs)t.Payload;

			NetEventReceived?.Invoke(this, args);
		}
	}
}
