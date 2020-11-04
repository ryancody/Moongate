using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class PingHandler : BaseHandler, IMessageHandler
	{
		public event EventHandler<PingArgs> PingReceived;

		public PingHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable m)
		{
			var pingArgs = (PingArgs)m.Payload;

			pingArgs.Ping = DateTimeOffset.Now.ToUnixTimeMilliseconds() - pingArgs.InitialTimestamp;

			Console.WriteLine($@"ping received - {pingArgs.Ping}ms");
			logger.Debug("Invoked Received Ping");

			PingReceived?.Invoke(this, pingArgs);
		}
	}
}