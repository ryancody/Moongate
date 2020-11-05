using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Telepathy;

namespace Moongate.Messaging.Listener
{
	public static class ServiceConfiguration
	{
		public static void AddMessageListener (this IServiceCollection services)
		{
			services.AddSingleton<IMessageListener, MessageListener>(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				var common = s.GetRequiredService<Common>();
				
				return new MessageListener(logger, common);
			});
		}
	}
}
