using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.StateController;
using Network.Factory;
using System;

namespace Moongate.Network.Events
{
	public class EventPublisher : EventPublisherBase
	{
		readonly ILogger logger;
		readonly IHandlerFactory handlerFactory;
		readonly GameStateController gameStateController;

		public override EventHandler<PingArgs> PingReceived { get; set; }
		public override EventHandler<EntityArgs> EntityAdded { get; set; }
		public override EventHandler<EntityArgs> EntityUpdated { get; set; }
		public override EventHandler<ControlArgs> PlayerInputChanged { get; set; }

		public EventPublisher (ILogger _logger, IHandlerFactory _handlerFactory, GameStateController _gameStateController) 
		{
			logger = _logger;
			handlerFactory = _handlerFactory;
			gameStateController = _gameStateController;

			((PingHandler)handlerFactory.GetHandler(MessageType.Ping)).PingReceived += OnPingReceived;
			gameStateController.EntityAdded += OnEntityAdded;
			gameStateController.EntityUpdated += OnEntityUpdated;
			((PlayerInputHandler)handlerFactory.GetHandler(MessageType.PlayerInput)).PlayerInputChanged += OnPlayerInputChanged;
		}

		public override void OnPlayerInputChanged (object sender, ControlArgs args)
		{
			PlayerInputChanged?.Invoke(this, args);
		}

		public override void OnPingReceived (object sender, PingArgs args)
		{
			PingReceived?.Invoke(this, args);
		}

		public override void OnEntityAdded (object sender, EntityArgs args)
		{
			EntityAdded?.Invoke(this, args);
		}

		public override void OnEntityUpdated (object sender, EntityArgs args)
		{
			EntityUpdated?.Invoke(this, args);
		}
	}
}
