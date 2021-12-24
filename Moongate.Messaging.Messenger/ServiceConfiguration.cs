using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Messaging.Messenger
{
	public static class ServiceConfiguration
	{
		public static void AddMessengerService(this IServiceCollection services, bool isServer)
		{
			if (isServer)
			{
				services.AddSingleton<IMessenger, ServerMessenger>();
			}
			else
			{
				services.AddSingleton<IMessenger, ClientMessenger>();
			}
		}
	}
}
