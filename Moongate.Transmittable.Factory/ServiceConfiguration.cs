using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.Logger;

namespace Moongate.Transmittable.Factory
{
	public static class ServiceConfiguration
	{
		public static void AddTransmittableFactory (this IServiceCollection services)
		{
			services.AddSingleton<ITransmittableFactory, TransmittableFactory>(s =>
			{
				var logger = s.GetRequiredService<ILogger>();
				var identityProvider = s.GetRequiredService<IIdentityProvider>();

				return new TransmittableFactory(logger, identityProvider);
			});
		}
	}
}
