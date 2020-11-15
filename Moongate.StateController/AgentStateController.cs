using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Transmittable.Factory;

namespace Moongate.StateController
{
	public class AgentStateController : IStateController
	{
		private readonly ILogger logger;
		private readonly PlayerStateController playerStateController;
		private readonly GameStateController gameStateController;
		private readonly ITransmittableFactory transmittableFactory;

		public AgentStateController (ILogger logger, 
			PlayerStateController playerStateController, 
			GameStateController gameStateController,
			IHandlerProvider handlerProvider, 
			ITransmittableFactory transmittableFactory)
		{
			this.logger = logger;
			this.playerStateController = playerStateController;
			this.gameStateController = gameStateController;
			this.transmittableFactory = transmittableFactory;

			handlerProvider.PlayerConnectedHandler.PlayerConnected += OnPlayerConnected;
			handlerProvider.PlayerHandshakeHandler.PlayerHandshake += OnPlayerHandshake;
			handlerProvider.EntityHandler.EntityReceived += OnEntityReceived;
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
			var t = transmittableFactory.Build(TransmissionType.GameStateRequest, gameStateController.GameState);

			//messenger.QueueTransmission(t);
		}

		private void OnEntityReceived (object sender, EntityArgs args)
		{
			gameStateController.ProcessEntity(args.Entity);
		}
	}
}
