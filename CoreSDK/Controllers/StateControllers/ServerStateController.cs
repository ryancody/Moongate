using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	public class ServerStateController : StateController
	{
		private readonly ILogger logger;
		private readonly PlayerManager playerManager;
		private readonly GameState gameState;

		public static event EventHandler GameStateRequested;

		public ServerStateController (ILogger l, PlayerManager pm, GameState gs)
		{
			logger = l;
			playerManager = pm;
			gameState = gs;


			PlayerHandshakeHandler.PlayerHandshaked += OnPlayerHandshake;
			CoreServer.PlayerConnected += OnPlayerConnected;
			GameStateRequestHandler.GameStateRequested += OnGameStateRequested;
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs args)
		{
			logger.Info($@"Player connected on {args.ConnectionId}");
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs args)
		{
			logger.Info($@"Player handshake.  Connection ID: {args.ConnectionId}");

			playerManager.AddPlayer(args.ConnectionId, args.Guid, args.Name);
		}

		protected virtual void OnPlayerDisconnected (object sender, PlayerConnectionArgs args)
		{
			var p = playerManager.GetPlayer(args.ConnectionId);

			logger.Info($@"{p.Name} disconnected from {args.ConnectionId}");
			playerManager.RemovePlayer(args.ConnectionId);
		}

		private void OnGameStateRequested (object sender)
		{
			var t = new Transmission(MessageType.GameStateRequest, e.Payload);
			messenger.Send(e.ConnectionId, t.Serialized());
		}
	}
}
