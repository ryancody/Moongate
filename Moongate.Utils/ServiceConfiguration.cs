using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Utils
{
	public static class ServiceConfiguration
	{
		public static void AddUtilityServices(this IServiceCollection services)
		{
			services.AddSingleton<ISerializer, Serializer>();
		}
	}
}
