using Network.Factory;
using Network.Models;
using System;

namespace Network.Controllers
{
	public class AgentStateController : IStateController
	{
		private readonly ILogger logger;
		private readonly PlayerStateController playerStateController;
		private readonly GameStateController gameStateController;
		private readonly IHandlerFactory handlerFactory;
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;

		public AgentStateController (ILogger _logger, 
			PlayerStateController _playerStateController, 
			GameStateController _gameStateController, 
			IHandlerFactory _handlerFactory, 
			IMessenger _messenger,
			ITransmittableFactory _transmittableFactory)
		{
			logger = _logger;
			playerStateController = _playerStateController;
			gameStateController = _gameStateController;
			messenger = _messenger;
			handlerFactory = _handlerFactory;
			transmittableFactory = _transmittableFactory;

			((PlayerConnectedHandler)handlerFactory.GetHandler(MessageType.PlayerConnected)).PlayerConnected += OnPlayerConnected;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(MessageType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
			((EntityHandler)handlerFactory.GetHandler(MessageType.EntityTransmit)).EntityReceived += OnEntityReceived;
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs args)
		{
			// player connects but only client cares, server waits for handshake
			//logger.Info($@"Player connected on {args.ConnectionId}");
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs args)
		{
			logger.Info($@"Player handshake.  Connection ID: {args.ConnectionId}");

			playerStateController.AddPlayer(args.ConnectionId, args.Guid, args.Name);
		}

		protected virtual void OnPlayerDisconnected (object sender, PlayerConnectionArgs args)
		{
			var p = playerStateController.GetPlayer(args.ConnectionId);

			logger.Info($@"{p.Name} disconnected from {args.ConnectionId}");
			playerStateController.RemovePlayer(args.ConnectionId);
		}

		private void OnGameStateRequested (object sender, GameStateRequestArgs args)
		{
			var t = transmittableFactory.Build(MessageType.GameStateRequest, gameStateController.GameState);

			messenger.QueueTransmission(t);
		}

		private void OnEntityReceived (object sender, EntityArgs args)
		{
			gameStateController.ProcessEntity(args.Entity);
		}
	}
}
