using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Models;
using Moongate.Transmittable.Factory;

namespace Moongate.Farspeaker
{
	public class Input
	{
		private readonly ILogger logger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IMessenger messenger;
		private readonly IIdentityProvider identityProvider;

		public Input (ILogger logger, ITransmittableFactory transmittableFactory, IMessenger messenger, IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;
			this.identityProvider = identityProvider;
		}

		public void Ping ()
		{
			var transmission = transmittableFactory.Build(TransmissionType.Ping, null);

			messenger.QueueTransmission(transmission);
		}

		public void PlayerInput (float x, float y, float magnitude)
		{
			var playerInput = new PlayerInputArgs
			{
				ControllerGuid = identityProvider.Id.Guid,
				Vector = new Vector
				{
					x = x,
					y = y,
					Magnitude = magnitude
				}
			};

			var transmission = transmittableFactory.Build(TransmissionType.PlayerInput, playerInput);

			messenger.QueueTransmission(transmission);
		}

		public void TransmitEntity (Entity entity)
		{
			var entityArgs = new EntityArgs
			{
				Entity = entity
			};

			var transmission = transmittableFactory.Build(TransmissionType.EntityTransmit, entityArgs);

			messenger.QueueTransmission(transmission);
		}

		public void RequestGameState ()
		{
			var gameStateArgs = new GameStateRequestArgs
			{
				SenderGuid = identityProvider.Id.Guid
			};

			var transmission = transmittableFactory.Build(TransmissionType.GameStateRequest, gameStateArgs);

			messenger.QueueTransmission(transmission);
		}
	}
}
