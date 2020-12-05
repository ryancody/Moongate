using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Utils
{
	public static class ServiceConfiguration
	{
		public static void AddSerializer (this IServiceCollection services)
		{
			services.AddSingleton<ISerializer>(s => 
			{
				return new Serializer();
			});
		}
	}
}
