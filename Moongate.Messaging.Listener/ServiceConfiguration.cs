using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;
using Telepathy;

namespace Moongate.Messaging.Listener
{
	public static class ServiceConfiguration
	{
		public static void AddMessageListener (this IServiceCollection services, bool isServer)
		{
			services.AddSingleton<IMessageListener, MessageListener>(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				Common common;

				if (isServer)
				{
					common = s.GetRequiredService<TelepathyServer>();
				}
				else
				{
					common = s.GetRequiredService<TelepathyClient>();
				}
				
				return new MessageListener(logger, common);
			});
		}
	}
}
