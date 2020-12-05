using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;

namespace Moongate.Logger
{
	public static class ServiceConfiguration
	{
		public static void AddLogger (this IServiceCollection services)
		{
			services.AddSingleton<ILogger, Logger>(s => 
			{
				var identityProvider = s.GetRequiredService<IIdentityProvider>();
				var agent = identityProvider.Id.IsServer ? "server" : "client";

				return new Logger(agent, new LoggerLevel[]{ LoggerLevel.Debug }, identityProvider);
			});
		}
	}
}
