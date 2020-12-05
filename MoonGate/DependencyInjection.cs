using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.IO;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.Transmittable.Processor;
using Moongate.Utils;
using System;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class DependencyInjection
	{
		public IServiceProvider ServiceProvider { get; set; }
		public IConfiguration Configuration { get; set; }

		public DependencyInjection (bool isServer)
		{
			ServiceProvider = ConfigureServices(BuildConfiguration(), isServer);
		}

		private IConfigurationRoot BuildConfiguration ()
		{
			return new ConfigurationBuilder().Build();
		}

		private IServiceProvider ConfigureServices (IConfigurationRoot configuration, bool isServer)
		{
			var services = new ServiceCollection();

			var loggerConfig = new LoggerConfig();
			services.AddLogger(loggerConfig);

			services.AddSerializer();

			if (isServer)
			{
				services.AddSingleton(s => 
				{
					return new TelepathyServer();
				});
			}
			else
			{
				services.AddSingleton(s => 
				{
					return new TelepathyClient();
				});
				services.AddFarspeaker();
			}

			services.AddMessageListener(isServer);

			services.AddMessageReceiver();

			services.AddTransmittableProcessor();

			services.AddHandlerFactory();

			services.AddMessenger(isServer);

			services.AddStateControllers();

			services.AddIdentityProvider();

			return services.BuildServiceProvider();
		}
	}
}
