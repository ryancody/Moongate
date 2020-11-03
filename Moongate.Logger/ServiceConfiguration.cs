using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Logger
{
	public static class ServiceConfiguration
	{
		public static void AddLogger (this IServiceCollection services, LoggerConfig config)
		{
			services.AddSingleton<ILogger, Logger>(s => 
			{
				return new Logger(config.User, new LoggerLevel[]{ config.LoggerLevel });
			});
		}
	}
}
