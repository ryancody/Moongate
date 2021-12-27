using Microsoft.Extensions.DependencyInjection;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.IO;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;
using Moongate.Transmittable.Processor;
using Moongate.Utils;
using System;
using Telepathy;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class DependencyInjection
	{
		public IServiceProvider ServiceProvider;

		public DependencyInjection (bool isServer)
		{
			ServiceProvider = BuildServices(isServer);
		}

		public IServiceProvider BuildServices (bool isServer)
		{
			var services = new ServiceCollection();

			if (isServer)
			{
				services.AddSingleton<Common, TelepathyServer>();
			}
			else
			{ 
				services.AddSingleton<Common, TelepathyClient>();
			}

			services.AddHandlerService();
			services.AddListenerService();
			services.AddMessengerService(isServer);
			services.AddReceiverService();
			services.AddStateControllerService();
			services.AddTransmittableFactoryService();
			services.AddTransmittableProcessorService();
			services.AddUtilityServices();
			services.AddFarspeakerService();
			services.AddIdentityProviderService(isServer);
			services.AddEventReactorService(isServer);
			services.AddLogging();

			return services.BuildServiceProvider();
		}
	}
}
