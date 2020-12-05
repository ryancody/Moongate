using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Identity.Provider
{
	public static class ServiceConfiguration
	{
		public static void AddIdentityProvider (this IServiceCollection services)
		{
			services.AddSingleton<IIdentityProvider>(s => 
			{
				return new IdentityProvider();
			});
		}
	}
}
