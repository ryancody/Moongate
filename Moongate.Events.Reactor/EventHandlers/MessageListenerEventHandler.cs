using Moongate.Identity.Provider;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.Events.Reactor.EventHandlers
{
	public class MessageListenerEventHandler : IEventHandler
	{
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IIdentityProvider identityProvider;

		public MessageListenerEventHandler (IMessageListener messageListener, 
			IMessenger messenger, 
			ITransmittableFactory transmittableFactory, 
			IIdentityProvider identityProvider)
		{
			this.messenger = messenger;
			this.transmittableFactory = transmittableFactory;
			this.identityProvider = identityProvider;

			messageListener.Connected += MessageListener_Connected;
			messageListener.Disconnected += MessageListener_Disconnected;
		}

		private void MessageListener_Connected (object sender, MessageArgs e)
		{
			if (identityProvider.Id.IsServer)
			{
				Console.WriteLine("player connected");
			}
			else
			{
				var playerHandshakeArgs = new PlayerHandshakeArgs
				{
					ConnectionId = e.SenderConnectionId,
					Guid = identityProvider.Id.Guid,
					Name = identityProvider.Id.Name
				};
				var transmission = transmittableFactory.Build(TransmissionType.PlayerHandshake, playerHandshakeArgs);

				messenger.QueueTransmission(transmission);
			}
		}

		private void MessageListener_Disconnected (object sender, MessageArgs e)
		{
			throw new NotImplementedException();
		}
	}
}
