using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Telepathy;

namespace CoreSDK.Controllers
{
	public class ServerMessenger : Messenger
	{
		readonly ILogger logger;
		readonly Server server;
		readonly PlayerManager playerManager;

		public ServerMessenger (ILogger l, Server s, PlayerManager pm)
		{
			logger = l;
			server = s;
			playerManager = pm;

			RequestPlayersListHandler.PlayersListRequest += OnPlayersListRequested;
			PingHandler.ReceivedPing += OnReceivedPing;
		}

		public override void Send (Transmission t)
		{
			throw new NotImplementedException();
		}

		public override void Send (int connectionId, Transmission message)
		{
			logger.Debug("Send - connectonId: " + connectionId + " - messageType: " + message.MessageType.ToString());
			server.Send(connectionId, message.Serialized());
		}

		public void Broadcast (Transmission t)
		{
			foreach (KeyValuePair<string, Player> p in playerManager.Players)
			{
				server.Send(p.Value.ConnectionId, t.Serialized());
			}
		}

		public void Relay (Transmission t)
		{
			foreach (Player p in playerManager.Players.Values.ToList())
			{
				if (p.ConnectionId != t.SenderConnectionId)
				{
					server.Send(p.ConnectionId, t.Serialized());
				}
			}
		}

		protected virtual void OnPlayersListRequested (object sender, BasicPlayerRequestArgs e)
		{
			var t = new Transmission(MessageType.RequestPlayersList, playerManager.Players.Values.ToList());

			server.Send(e.ConnectionId, t.Serialized());
		}

		protected virtual void OnReceivedPing (object sender, PingArgs e)
		{
			var t = new Transmission(MessageType.Pong, e);

			server.Send(e.ConnectionId, t.Serialized());
		}
	}
}
