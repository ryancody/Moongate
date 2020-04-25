using CoreSDK.Factory;
using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	public class ServerStateController : IStateController
	{
		private readonly ILogger logger;
		private readonly PlayerState playerState;
		private readonly GameState gameState;
		private readonly IHandlerFactory handlerFactory;
		private readonly IServerMessenger messenger;

		public ServerStateController (ILogger _logger, PlayerState _playerState, GameState _gameState, IHandlerFactory _handlerFactory, IServerMessenger _messenger)
		{
			logger = _logger;
			playerState = _playerState;
			gameState = _gameState;
			messenger = _messenger;
			handlerFactory = _handlerFactory;

			((PlayerConnectedHandler)handlerFactory.GetHandler(MessageType.PlayerConnected)).PlayerConnected += OnPlayerConnected;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(MessageType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs args)
		{
			logger.Info($@"Player connected on {args.ConnectionId}");
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs args)
		{
			logger.Info($@"Player handshake.  Connection ID: {args.ConnectionId}");

			playerState.AddPlayer(args.ConnectionId, args.Guid, args.Name);
		}

		protected virtual void OnPlayerDisconnected (object sender, PlayerConnectionArgs args)
		{
			var p = playerState.GetPlayer(args.ConnectionId);

			logger.Info($@"{p.Name} disconnected from {args.ConnectionId}");
			playerState.RemovePlayer(args.ConnectionId);
		}

		private void OnGameStateRequested (object sender, GameStateRequestArgs args)
		{
			var t = new Transmission(MessageType.GameStateRequest, gameState);

			messenger.Transmit(args.RequestedBy, t);
		}
	}
}
