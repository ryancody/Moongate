using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class PingHandler : IMessageHandler
	{
		private ILogger logger;

		public static event EventHandler<PingArgs> ReceivedPing;

		public PingHandler (ILogger logger)
		{
			this.logger = logger;
		}

		public void Handle (Transmission m)
		{
			var ping = new PingArgs()
			{
				ConnectionId = m.SenderConnectionId,
				InitialTimestamp = m.Timestamp
			};

			ReceivedPing?.Invoke(this, ping);
		}
	}
}