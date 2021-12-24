using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moongate.Messaging.Handler
{
	public static class ServiceConfiguration
	{
		public static void AddHandlerService(this IServiceCollection services)
		{
			services.AddSingleton<PlayerConnectedHandler>();
			services.AddSingleton<PlayerHandshakeHandler>();
			services.AddSingleton<PlayerDisconnectedHandler>();
			services.AddSingleton<PlayerInputHandler>();
			services.AddSingleton<GameStateRequestHandler>();
			services.AddSingleton<EntityHandler>();
			services.AddSingleton<PingHandler>();

			services.AddSingleton<IHandlerProvider, HandlerProvider>();
		}
	}
}
