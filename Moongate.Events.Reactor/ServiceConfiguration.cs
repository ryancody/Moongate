using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;

namespace Moongate.Events.Reactor
{
	public static class ServiceConfiguration
	{
		public static void AddEventReactor (this IServiceCollection services)
		{
			services.AddSingleton(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				var handlerProvider = s.GetRequiredService<IHandlerProvider>();
				var gameStateController = s.GetRequiredService<GameStateController>();
				var playerStateController = s.GetRequiredService<PlayerStateController>();
				var transmittableFactory = s.GetRequiredService<ITransmittableFactory>();
				var messenger = s.GetRequiredService<IMessenger>();
				var messageListener = s.GetRequiredService<IMessageListener>();
				var identityProvider = s.GetRequiredService<IIdentityProvider>();

				return new EventReactor(logger, 
					handlerProvider, 
					gameStateController, 
					playerStateController, 
					transmittableFactory, 
					messageListener, 
					messenger, 
					identityProvider);
			});
		}
	}
}
