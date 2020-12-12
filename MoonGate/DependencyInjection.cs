using Microsoft.Extensions.DependencyInjection;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.IO;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.Transmittable.Factory;
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

		public DependencyInjection (bool isServer)
		{
			ServiceProvider = ConfigureServices(isServer);
		}

		private IServiceProvider ConfigureServices (bool isServer)
		{
			var services = new ServiceCollection();

			services.AddLogger();

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

			services.AddTransmittableFactory();

			services.AddHandlerFactory();

			services.AddMessenger(isServer);

			services.AddStateControllers();

			services.AddIdentityProvider(isServer);

			services.AddEventReactor();

			return services.BuildServiceProvider();
		}
	}
}
