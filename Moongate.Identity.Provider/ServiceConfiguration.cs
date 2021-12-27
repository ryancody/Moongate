using Microsoft.Extensions.DependencyInjection;
using Moongate.Models.Identity;

namespace Moongate.Identity.Provider
{
	public static class ServiceConfiguration
	{
		public static void AddIdentityProviderService(this IServiceCollection services, bool isServer)
		{
			services.AddSingleton<IIdentityProvider, IdentityProvider>(s =>
			{
				return new IdentityProvider
				{
					Id = new Id
					{
						IsServer = isServer
					}
				};
			});
		}
	}
}
