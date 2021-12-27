using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moongate.Messaging.Handler;
using Moongate.State.Models;

namespace Moongate.State.Controller
{
	public static class ServiceConfiguration
	{
		public static void AddStateControllerService(this IServiceCollection services)
		{
			services.AddSingleton(s =>
			{
				var logger = s.GetRequiredService<ILogger<PlayerStateController>>();
				var playerState = new PlayerState();
				return new PlayerStateController(logger, playerState);
			});
		}
	}
}
