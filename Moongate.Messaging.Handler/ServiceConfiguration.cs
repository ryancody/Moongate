using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Messaging.Handler
{
    public static class ServiceConfiguration
	{
		public static void AddHandlerService(this IServiceCollection services)
		{
			services.AddSingleton<PlayerConnectedHandler>();
			services.AddSingleton<PlayerHandshakeHandler>();
			services.AddSingleton<PlayerDisconnectedHandler>();
			services.AddSingleton<PingHandler>();
			services.AddSingleton<NetEventHandler>();

			services.AddSingleton<IHandlerProvider, HandlerProvider>();
		}
	}
}
