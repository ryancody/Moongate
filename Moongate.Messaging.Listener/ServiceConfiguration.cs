using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Messaging.Listener
{
	public static class ServiceConfiguration
	{
		public static void AddListenerService(this IServiceCollection services)
		{
			services.AddSingleton<IMessageListener, MessageListener>();
		}
	}
}
