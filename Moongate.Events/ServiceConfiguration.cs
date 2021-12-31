using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Events
{
    public static class ServiceConfiguration
	{
		public static void AddEventHandlerService(this IServiceCollection services, bool isServer)
		{
			if (isServer)
			{
				services.AddSingleton<IEventHandler, ServerEventHandler>();
			}
			else {
				services.AddSingleton<IEventHandler, ClientEventHandler>();
			}
		}
	}
}
