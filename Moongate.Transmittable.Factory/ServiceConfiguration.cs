using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Transmittable.Factory
{
	public static class ServiceConfiguration
	{
		public static void AddTransmittableFactoryService(this IServiceCollection services)
		{
			services.AddSingleton<ITransmittableFactory, TransmittableFactory>();
		}
	}
}
