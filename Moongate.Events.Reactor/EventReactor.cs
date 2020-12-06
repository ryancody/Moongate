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
		private readonly ISet<IEventHandler> eventHandlers;

		public EventReactor (ILogger logger, 
			IHandlerProvider handlerProvider, 
			GameStateController gameStateController, 
			PlayerStateController playerStateController,
			ITransmittableFactory transmittableFactory, 
			IMessageListener messageListener,
			IMessenger messenger,
			IIdentityProvider identityProvider)
		{
			eventHandlers = new HashSet<IEventHandler>
			{
				new MessageListenerEventHandler(logger, messageListener, messenger, transmittableFactory, identityProvider)
			};

			if (identityProvider.Id.IsServer)
			{
				eventHandlers.Add(new ServerHandlerProviderEventHandler(logger, handlerProvider, messenger, transmittableFactory, identityProvider, gameStateController, playerStateController));
			}
			else
			{
				eventHandlers.Add(new ClientHandlerProviderEventHandler(logger, handlerProvider, messenger, transmittableFactory, identityProvider, gameStateController, playerStateController));
			}
		}
	}
}
