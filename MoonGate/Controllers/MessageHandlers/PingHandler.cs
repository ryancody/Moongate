using Network.Controllers;
using Network.Models;
using System;
using System.Net.NetworkInformation;

namespace Network
{
	public class PingHandler : IMessageHandler
	{
		readonly ILogger logger;

		public event EventHandler<PingArgs> PingReceived;

		public PingHandler (ILogger logger)
		{
			this.logger = logger;
		}

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