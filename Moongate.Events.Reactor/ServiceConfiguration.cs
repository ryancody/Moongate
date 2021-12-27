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
				services.AddSingleton<IHandlerProviderEventHandler, ServerHandlerProviderEventHandler>();
				services.AddSingleton<IMessageListenerEventHandler, ServerMessageListenerEventHandler>();
			}
			else {
				services.AddSingleton<IHandlerProviderEventHandler, ClientHandlerProviderEventHandler>();
				services.AddSingleton<IMessageListenerEventHandler, ClientMessageListenerEventHandler>();
			}
			services.AddSingleton<EventReactor>(s => 
			{
				var messageListenerEventHandler = s.GetRequiredService<IMessageListenerEventHandler>();
				var handlerProviderEventHandler = s.GetRequiredService<IHandlerProviderEventHandler>();
				return new EventReactor(messageListenerEventHandler, handlerProviderEventHandler);
			});
		}
	}
}
