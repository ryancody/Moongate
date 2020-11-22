using Microsoft.Extensions.DependencyInjection;
using Moongate.State.Models;
using Moongate.StateController;

namespace Moongate.Logger
{
	public static class ServiceConfiguration
	{
		public static void AddStateController (this IServiceCollection services)
		{
			services.AddSingleton<PlayerStateController>(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				return new PlayerStateController(logger, new PlayerState());
			});

			services.AddSingleton<GameStateController>(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				return new GameStateController(logger, new GameState());
			});
		}
	}
}
