using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Moongate.State.Controller;
using Moongate.Utils;

namespace Moongate.Messaging.Messenger
{
	public static class ServiceConfiguration
	{
		public static void AddMessenger (this IServiceCollection services, bool isServer)
		{
			if (isServer)
			{
				services.AddSingleton<IMessenger, ServerMessenger>(s =>
				{
					var logger = s.GetService<ILogger>();
					var serializer = s.GetRequiredService<ISerializer>();
					var telepathyServer = s.GetRequiredService<Telepathy.Server>();
					var playerStateController = s.GetRequiredService<PlayerStateController>();

					return new ServerMessenger(logger, serializer, telepathyServer, playerStateController);
				});
			}
			else
			{
				services.AddSingleton<IMessenger, ClientMessenger>(s =>
				{
					var logger = s.GetService<ILogger>();
					var serializer = s.GetRequiredService<ISerializer>();
					var telepathyClient = s.GetRequiredService<Telepathy.Client>();

					return new ClientMessenger(logger, serializer, telepathyClient);
				});
			}
		}
	}
}
