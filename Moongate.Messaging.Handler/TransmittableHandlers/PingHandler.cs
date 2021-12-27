using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PingHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<PingArgs> PingReceived;

		public PingHandler () : base() { }

		public void Handle (ITransmittable t)
		{
			var pingArgs = (PingArgs)t.Payload;

			PingReceived?.Invoke(this, pingArgs);
		}
	}
}