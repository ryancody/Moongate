using CoreSDK.Models;
using System;
using Telepathy;
using CoreSDK;

namespace CoreSDK.Controllers
{
	public class ClientMessenger : Messenger
	{
		readonly ILogger logger;
		readonly Client client;

		public ClientMessenger (ILogger l, Client c)
		{
			logger = l;
			client = c;

			CoreClient.ConnectedToServer += new EventHandler<PlayerConnectionArgs>(OnConnectedToServer);
		}

		public override void Send (Transmission message)
		{
			logger.Debug("Send - messageType: " + message.MessageType.ToString());
			client.Send(message.Serialized());
		}

		public override void Send (int i, Transmission t)
		{
			throw new NotImplementedException();
		}

		public void InitiateHandshake ()
		{
			var p = new PlayerHandshakeArgs()
			{
				Name = LocalId.Name,
				Guid = LocalId.Guid
			};
			var t = new Transmission(MessageType.PlayerHandshake, p);

			Send(t);
		}

		public void Ping ()
		{
			var t = new Transmission(MessageType.Ping, null);
			client.Send(t.Serialized());
		}

		// when this client creates an entity
		public void OnEntityCreated (object sender, CreateEntityArgs args)
		{
			var entity = (Entity)args.Payload;
			var t = new Transmission(MessageType.CreateEntity, entity);

			client.Send(t.Serialized());
		}

		protected void OnConnectedToServer (object sender, PlayerConnectionArgs args)
		{
			logger.Info("Connected to server, handshaking...");

			InitiateHandshake();
		}
	}
}
