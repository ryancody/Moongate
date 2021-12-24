using Microsoft.Extensions.DependencyInjection;
using Moongate.Events.Reactor.EventHandlers;

namespace Moongate.Events.Reactor
{
	public static class ServiceConfiguration
	{
		public static void AddEventReactorService(this IServiceCollection services, bool isServer)
		{
			if (isServer)
			{
				services.AddSingleton<IEventHandler, ServerHandlerProviderEventHandler>();
			}
			else {
				services.AddSingleton<IEventHandler, ClientHandlerProviderEventHandler>();
			}
			services.AddSingleton<MessageListenerEventHandler>();
			services.AddSingleton<EventReactor>();
		}
	}
}
