using CoreSDK.Factory;
using CoreSDK.Models;

namespace CoreSDK.Controllers
{
	public class ServerStateController : IStateController
	{
		private readonly ILogger logger;
		private readonly PlayerStateController playerStateController;
		private readonly GameStateController gameStateController;
		private readonly IHandlerFactory handlerFactory;
		private readonly IServerMessenger messenger;

		public ServerStateController (ILogger _logger, PlayerStateController _playerStateController, GameStateController _gameStateController, IHandlerFactory _handlerFactory, IServerMessenger _messenger)
		{
			logger = _logger;
			playerStateController = _playerStateController;
			gameStateController = _gameStateController;
			messenger = _messenger;
			handlerFactory = _handlerFactory;

			((PlayerConnectedHandler)handlerFactory.GetHandler(MessageType.PlayerConnected)).PlayerConnected += OnPlayerConnected;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(MessageType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
			((EntityHandler)handlerFactory.GetHandler(MessageType.EntityUpdate)).EntityReceived += OnEntityReceived;
		}

		private void OnPlayerConnected (object sender, PlayerConnectionArgs args)
		{
			logger.Info($@"Player connected on {args.ConnectionId}");
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
			var t = new Transmission(MessageType.GameStateRequest, playerStateController);

			messenger.Transmit(args.RequestedBy, t);
		}

		private void OnEntityReceived (object sender, EntityArgs args)
		{
			if (gameStateController.HasEntity(args.Entity))
			{
				gameStateController.UpdateEntity(args.Entity);

				// invoke entity updated
			}
			else
			{
				gameStateController.AddEntity(args.Entity);

				// invoke entity added
			}
		}
	}
}
