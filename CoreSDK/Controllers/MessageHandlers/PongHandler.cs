using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class PongHandler : IMessageHandler
	{
		private ILogger logger;

		public static event EventHandler<PingArgs> ReceivedPong;

		public PongHandler (ILogger logger)
		{
			this.logger = logger;
		}

		public void Handle (Transmission m)
		{
			var p = (PingArgs)m.Payload;
			var now = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			var latency = now - p.InitialTimestamp;
			p.Ping = latency;

			logger.Debug($@"ping: {p.InitialTimestamp} - {now} = {latency}");

			ReceivedPong?.Invoke(this, p);
		}
	}
}