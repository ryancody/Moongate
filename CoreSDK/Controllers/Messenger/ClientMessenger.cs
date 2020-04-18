using CoreSDK.Models;
using System;
using Telepathy;
using CoreSDK;

namespace CoreSDK.Controllers
{
	public class ClientMessenger : IClientMessenger
	{
		readonly ILogger logger;
		readonly Client client;

		public ClientMessenger (ILogger l, Client c)
		{
			logger = l;
			client = c;

			CoreClient.ConnectedToServer += ConnectedToServer;
			PingHandler.PingReceived += OnPingReceived;
		}

		private void OnPingReceived (object sender, PingArgs args)
		{
			var ping = DateTimeOffset.Now.ToUnixTimeMilliseconds() - args.InitialTimestamp;

			Console.WriteLine(ping + "ms");
			logger.Debug($@"{ping}ms");
		}

		private void ConnectedToServer (object sender, EventArgs e)
		{
			InitiateHandshake();
		}

		public void Transmit (Transmission t)
		{
			logger.Debug("Send - messageType: " + t.MessageType.ToString());

			client.Send(t.Serialized());
		}

		public void InitiateHandshake ()
		{
			logger.Debug("initiating handshake");

			var p = new PlayerHandshakeArgs()
			{
				Name = LocalId.Name,
				Guid = LocalId.Guid
			};
			var t = new Transmission(MessageType.PlayerHandshake, p);

			Transmit(t);
		}

		public void Ping ()
		{
			var args = new PingArgs()
			{
				InitialTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
			};
			var t = new Transmission(MessageType.Ping, args);
		 
			client.Send(t.Serialized());
		}
	}
}
