using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Messenger;
using Moongate.Transmittable.Factory;

namespace Moongate.IO
{
	public static class ServiceConfiguration
	{
		public static void AddFarspeaker (this IServiceCollection services)
		{
			services.AddSingleton(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				var transmittableFactory = s.GetRequiredService<ITransmittableFactory>();
				var messenger = s.GetRequiredService<IMessenger>();
				var identityProvider = s.GetRequiredService<IIdentityProvider>();
				var handlerProvider = s.GetRequiredService<IHandlerProvider>();

				return new Farspeaker(logger, transmittableFactory, messenger, identityProvider, handlerProvider);
			});
		}
	}
}
