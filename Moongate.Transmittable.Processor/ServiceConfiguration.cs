using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Transmittable.Processor
{
	public static class ServiceConfiguration
	{
		public static void AddTransmittableProcessorService(this IServiceCollection services)
		{
			services.AddSingleton<ITransmittableProcessor, TransmittableProcessor>();
		}
	}
}
