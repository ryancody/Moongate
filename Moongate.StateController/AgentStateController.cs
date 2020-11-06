using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using Moongate.TransmittableFactory;

namespace Moongate.StateController
{
	public class AgentStateController : IStateController
	{
		private readonly ILogger logger;
		private readonly PlayerStateController playerStateController;
		private readonly GameStateController gameStateController;
		private readonly IHandlerFactory handlerFactory;
		private readonly ITransmittableFactory transmittableFactory;

		public AgentStateController (ILogger logger, 
			PlayerStateController playerStateController, 
			GameStateController gameStateController, 
			IHandlerFactory handlerFactory, 
			ITransmittableFactory transmittableFactory)
		{
			this.logger = logger;
			this.playerStateController = playerStateController;
			this.gameStateController = gameStateController;
			this.handlerFactory = handlerFactory;
			this.transmittableFactory = transmittableFactory;

			((PlayerConnectedHandler)handlerFactory.GetHandler(TransmissionType.PlayerConnected)).PlayerConnected += OnPlayerConnected;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(TransmissionType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
			((EntityHandler)handlerFactory.GetHandler(TransmissionType.EntityTransmit)).EntityReceived += OnEntityReceived;
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
