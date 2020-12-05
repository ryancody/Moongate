using Microsoft.Extensions.DependencyInjection;
using Moongate.Messaging.Handler;
using Moongate.State.Controller;
using Moongate.State.Models;

namespace Moongate.Logger
{
	public static class ServiceConfiguration
	{
		public static void AddStateControllers (this IServiceCollection services)
		{
			services.AddSingleton(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				return new PlayerStateController(logger, new PlayerState());
			});

			services.AddSingleton(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				var handlerProvider = s.GetRequiredService<IHandlerProvider>();
				return new GameStateController(logger, new GameState(), handlerProvider);
			});
		}
	}
}
