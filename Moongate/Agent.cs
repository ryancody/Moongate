using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.Transmittable.Processor;
using System;

namespace Moongate
{
	public class Agent
	{
		protected readonly DependencyInjection dependencyInjection;
		protected readonly ILogger<Agent> logger;
		protected readonly IMessageListener messageListener;
		protected readonly IMessageReceiver messageReceiver;
		protected readonly ITransmittableProcessor transmittableProcessor;
		protected readonly IHandlerProvider handlerProvider;
		protected readonly EventReactor eventReactor;
		protected readonly IMessenger messenger;
		protected readonly IIdentityProvider identityProvider;

		internal Agent (bool isServer)
		{
			dependencyInjection = new DependencyInjection(isServer);

			logger = dependencyInjection.ServiceProvider.GetRequiredService<ILogger<Agent>>();
			messageListener = dependencyInjection.ServiceProvider.GetRequiredService<IMessageListener>();
			messageReceiver = dependencyInjection.ServiceProvider.GetRequiredService<IMessageReceiver>();
			transmittableProcessor = dependencyInjection.ServiceProvider.GetRequiredService<ITransmittableProcessor>();
			handlerProvider = dependencyInjection.ServiceProvider.GetRequiredService<IHandlerProvider>();
			eventReactor = dependencyInjection.ServiceProvider.GetRequiredService<EventReactor>();
			messenger = dependencyInjection.ServiceProvider.GetRequiredService<IMessenger>();
			identityProvider = dependencyInjection.ServiceProvider.GetRequiredService<IIdentityProvider>();
		}

		public void Run ()
		{
			try
			{
				messageListener.Listen();

				messenger.TransmitQueue();
			}
			catch (Exception e)
			{
				logger.LogError(e.ToString());
				Console.WriteLine(e);
			}
		}
	}
}
