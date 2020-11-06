using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Moongate.Messaging.Handler;

namespace Moongate.Messaging.Receiver
{
	public static class ServiceConfiguration
	{
		public static void AddHandlerFactory (this IServiceCollection services)
		{
			services.AddSingleton<IHandlerProvider, HandlerProvider>(s => 
			{
				var logger = s.GetRequiredService<ILogger>();

				return new HandlerProvider(logger);
			});
		}
	}
}
