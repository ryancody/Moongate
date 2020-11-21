using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public static class ServiceConfiguration
	{
		public static void AddClient (this IServiceCollection services)
		{
			services.AddSingleton(c => 
			{
				var logger = c.GetRequiredService<ILogger>();
				var telepathy = c.GetRequiredService<TelepathyClient>();
				var messageListener = c.GetRequiredService<IMessageListener>();
				var messenger = c.GetRequiredService<IMessenger>();
				var identityProvider = c.GetRequiredService<IIdentityProvider>();

				return new Client(logger, telepathy, messageListener, messenger, identityProvider);
			});
		}

		public static void AddServer (this IServiceCollection services)
		{
			services.AddSingleton(s =>
			{
				var logger = s.GetRequiredService<ILogger>();
				var telepathy = s.GetRequiredService<TelepathyServer>();
				var messageListener = s.GetRequiredService<IMessageListener>();
				var messenger = s.GetRequiredService<IMessenger>();
				var identityProvider = s.GetRequiredService<IIdentityProvider>();

				return new Server(logger, telepathy, messenger, messageListener, identityProvider);
			});
		}
	}
}
