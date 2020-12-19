using Moongate.Events.Reactor.EventHandlers;
using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;
using System.Collections.Generic;

namespace Moongate.Events.Reactor
{
	public class EventReactor
	{
		private readonly ISet<IEventHandler> eventHandlers = new HashSet<IEventHandler>();

		public EventReactor (ILogger logger, 
			IHandlerProvider handlerProvider,
			PlayerStateController playerStateController,
			ITransmittableFactory transmittableFactory, 
			IMessageListener messageListener,
			IMessenger messenger,
			IIdentityProvider identityProvider)
		{
			if (identityProvider.Id.IsServer)
			{
				eventHandlers.Add(new ServerHandlerProviderEventHandler(logger, handlerProvider, messenger, transmittableFactory, playerStateController));
				eventHandlers.Add(new ServerMessageListenerEventHandler(logger, messageListener, transmittableFactory, messenger, playerStateController));
			}
			else
			{
				eventHandlers.Add(new ClientHandlerProviderEventHandler(logger, handlerProvider, identityProvider, playerStateController));
				eventHandlers.Add(new ClientMessageListenerEventHandler(logger, messageListener, messenger, transmittableFactory, identityProvider));
			}
		}
	}
}
