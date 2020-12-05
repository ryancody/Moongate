using Microsoft.Extensions.DependencyInjection;
using Moongate.Models.Identity;

namespace Moongate.Identity.Provider
{
	public static class ServiceConfiguration
	{
		public static void AddIdentityProvider (this IServiceCollection services)
		{
			services.AddSingleton<IIdentityProvider, IdentityProvider>(s =>
			{
				return new IdentityProvider
				{
					Id = new Id()
				};
			});
		}
	}
}
