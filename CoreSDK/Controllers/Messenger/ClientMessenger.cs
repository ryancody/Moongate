using CoreSDK.Models;
using System;
using Telepathy;
using CoreSDK;
using CoreSDK.Factory;
using CoreSDK.Utils;

namespace CoreSDK.Controllers
{
	public class ClientMessenger : IClientMessenger
	{
		readonly Client client;
		readonly ILogger logger;
		readonly ITransmittableFactory transmittableFactory;
		readonly ISerializer serializer;

		public ClientMessenger (ILogger _logger, Client _client, ITransmittableFactory _transmittableFactory, ISerializer _serializer)
		{
			logger = _logger;
			client = _client;
			transmittableFactory = _transmittableFactory;
			serializer = _serializer;

			CoreClient.ConnectedToServer += CoreClient_ConnectedToServer;
		}

		private void CoreClient_ConnectedToServer (object sender, EventArgs e)
		{
			InitiateHandshake();
		}

		public void Transmit (ITransmittable t)
		{
			var serTransmit = serializer.Serialize(t);
			
			logger.Debug("Send - messageType: " + t.MessageType.ToString());

			client.Send(serTransmit);
		}

		public void InitiateHandshake ()
		{
			logger.Debug("initiating handshake");

			var args = new PlayerHandshakeArgs()
			{
				Name = LocalId.Name,
				Guid = LocalId.Guid
			};
			var t = transmittableFactory.Build(MessageType.PlayerHandshake, args);

			Transmit(t);
		}

		public void Ping ()
		{
			var args = new PingArgs()
			{
				InitialTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
			};
			var t = transmittableFactory.Build(MessageType.Ping, args);

			Transmit(t);
		}
	}
}
