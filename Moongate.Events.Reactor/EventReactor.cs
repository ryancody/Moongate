using Moongate.Events.Reactor.EventHandlers;
using System.Collections.Generic;

namespace Moongate.Events.Reactor
{
	public class EventReactor
	{
		private readonly ISet<IEventHandler> eventHandlers;

		public EventReactor (MessageListenerEventHandler messageListenerEventHandler, IEventHandler eventHandler)
		{
			eventHandlers = new HashSet<IEventHandler>
			{
				messageListenerEventHandler,
				eventHandler
			};
		}
	}
}
