using Moongate.Logger;
using Moongate.Messenger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using Moongate.TransmittableFactory;

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

		public void OnPlayerInputChanged (object sender, ControlArgs e)
		{
			var t = transmittableFactory.Build(MessageType.PlayerInput, e);
			messenger.QueueTransmission(t);
		}

		public void OnEntityUpdate (object sender, EntityArgs e)
		{
			var t = transmittableFactory.Build(MessageType.EntityTransmit, e);
			messenger.QueueTransmission(t);
		}
	}
}
