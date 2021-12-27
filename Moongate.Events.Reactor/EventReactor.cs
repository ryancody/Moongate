using Moongate.Events.Reactor.EventHandlers;
using System.Collections.Generic;

namespace Moongate.Events.Reactor
{
	public class EventReactor
	{
		private readonly ISet<IEventHandler> eventHandlers = new HashSet<IEventHandler>();

		public EventReactor (IMessageListenerEventHandler messageListenerEventHandler, IHandlerProviderEventHandler handlerProvider)
		{
			eventHandlers = new HashSet<IEventHandler>
			{
				messageListenerEventHandler,
				handlerProvider
			};
		}
	}
}
