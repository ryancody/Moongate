using CoreSDK.Controllers;
using CoreSDK.Factory;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public class EventPublisher : EventPublisherBase
	{
		readonly ILogger logger;
		readonly IHandlerFactory handlerFactory;
		readonly GameStateController gameStateController;

		public override EventHandler<PingArgs> PingReceived { get; set; }
		public override EventHandler<EntityArgs> EntityAdded { get; set; }
		public override EventHandler<EntityArgs> EntityUpdated { get; set; }
		public override EventHandler<PlayerInputArgs> PlayerInput { get; set; }

		public EventPublisher (ILogger _logger, IHandlerFactory _handlerFactory, GameStateController _gameStateController) 
		{
			logger = _logger;
			handlerFactory = _handlerFactory;
			gameStateController = _gameStateController;

			((PingHandler)handlerFactory.GetHandler(MessageType.Ping)).PingReceived += OnPingReceived;
			gameStateController.EntityAdded += OnEntityAdded;
			gameStateController.EntityUpdated += OnEntityUpdated;
			((PlayerInputHandler)handlerFactory.GetHandler(MessageType.PlayerInput)).PlayerInput += OnPlayerInput;
		}

		public override void OnPlayerInput (object sender, PlayerInputArgs args)
		{
			PlayerInput?.Invoke(this, args);
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
