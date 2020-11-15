using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Receiver;
using Moongate.Transmittable.Processor;
using System;
using Telepathy;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class Startup
	{
		public IServiceProvider ServiceProvider { get; set; }
		public IConfiguration Configuration { get; set; }

		private readonly bool isServer = false;

		public Startup ()
		{
			var configuration = new ConfigurationBuilder();

			configuration.Build();

			var serviceCollection = new ServiceCollection();

			ServiceProvider = serviceCollection.BuildServiceProvider();
		}

		public void ConfigureServices (IServiceCollection services)
		{
			var loggerConfig = new LoggerConfig();
			services.AddLogger(loggerConfig);

			if (isServer)
			{
				services.AddSingleton(typeof(Common), new TelepathyServer());
			}
			else
			{
				services.AddSingleton(typeof(Common), new TelepathyClient());
			}

			services.AddMessageListener();

			services.AddMessageReceiver();

			services.AddTransmittableProcessor();

			services.AddHandlerFactory();

			services.AddStateController();


		}
	}
}
