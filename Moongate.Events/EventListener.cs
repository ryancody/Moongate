using Moongate.Logger;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Transmittable.Factory;

namespace Moongate.Network.Events
{
	public class EventListener
	{
		readonly ILogger logger;
		readonly ITransmittableFactory transmittableFactory;
		readonly IMessenger messenger;

		public EventListener (ILogger logger, ITransmittableFactory transmittableFactory, IMessenger messenger) 
		{
			this.logger = logger;
			this.transmittableFactory = transmittableFactory;
			this.messenger = messenger;
		}

		public void OnPlayerInputChanged (object sender, PlayerInputArgs e)
		{
			var t = transmittableFactory.Build(TransmissionType.PlayerInput, e);
			messenger.QueueTransmission(t);
		}

		public void OnEntityUpdate (object sender, EntityArgs e)
		{
			var t = transmittableFactory.Build(TransmissionType.EntityTransmit, e);
			messenger.QueueTransmission(t);
		}
	}
}
