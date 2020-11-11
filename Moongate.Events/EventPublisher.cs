using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Models.Events;
using Moongate.StateController;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Network.Events
{
	public class EventPublisher : EventPublisherBase
	{
		readonly ILogger logger;
		readonly IHandlerProvider handlerProvider;
		readonly GameStateController gameStateController;

		public override EventHandler<PingArgs> PingReceived { get; set; }
		public override EventHandler<EntityArgs> EntityAdded { get; set; }
		public override EventHandler<EntityArgs> EntityUpdated { get; set; }
		public override EventHandler<ControlArgs> PlayerInputChanged { get; set; }

		public EventPublisher (ILogger logger, IHandlerProvider handlerProvider, GameStateController gameStateController) 
		{
			this.logger = logger;
			this.handlerProvider = handlerProvider;
			this.gameStateController = gameStateController;

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerInputHandler.PlayerInputChanged += OnPlayerInputChanged;
			gameStateController.EntityAdded += OnEntityAdded;
			gameStateController.EntityUpdated += OnEntityUpdated;

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
