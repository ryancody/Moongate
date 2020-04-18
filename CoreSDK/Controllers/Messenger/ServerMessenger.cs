using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telepathy;

namespace CoreSDK.Controllers
{
	public class ServerMessenger : IServerMessenger
	{
		readonly ILogger logger;
		readonly Server server;
		readonly PlayerManager playerManager;

		public ServerMessenger (ILogger l, Server s, PlayerManager pm)
		{
			logger = l;
			server = s;
			playerManager = pm;

			PingHandler.PingReceived += OnReceivedPing;
			PlayerHandshakeHandler.PlayerHandshaked += OnPlayerJoined;
		}

		private void OnPlayerJoined (object sender, PlayerHandshakeArgs args)
		{
			var t = new Transmission(MessageType.PlayerConnected, args);

			Broadcast(t);
		}

		protected void OnReceivedPing (object sender, PingArgs args)
		{
			var t = new Transmission(MessageType.Ping, args);

			Transmit(args.ConnectionId, t);
		}

		public void Broadcast (Transmission t)
		{

			playerManager.Players.Values.ToList().ForEach(p =>
			{
				Transmit(p.ConnectionId, t);
			});
		}

		public void Relay (Transmission t, string guid)
		{
			var toId = playerManager.GetPlayer(guid).ConnectionId;

			Transmit(toId, t);
		}

		public void Transmit (int connectionId, Transmission message)
		{
			logger.Debug("Send - connectonId: " + connectionId + " - messageType: " + message.MessageType.ToString());

			server.Send(connectionId, message.Serialized());
		}
	}
}
