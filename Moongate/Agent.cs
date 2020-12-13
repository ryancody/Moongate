using DependencyInjection;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.Logger;
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
		protected readonly ILogger logger;
		protected readonly IMessageListener messageListener;
		protected readonly IMessageReceiver messageReceiver;
		protected readonly ITransmittableProcessor transmittableProcessor;
		protected readonly IHandlerProvider handlerProvider;
		protected readonly EventReactor eventReactor;
		protected readonly IMessenger messenger;
		protected readonly IIdentityProvider identityProvider;

		public Agent (bool isServer)
		{
			dependencyInjection = new DependencyInjection(isServer);

			logger = dependencyInjection.Services.GetService<ILogger>();
			messageListener = dependencyInjection.Services.GetService<IMessageListener>();
			messageReceiver = dependencyInjection.Services.GetService<IMessageReceiver>();
			transmittableProcessor = dependencyInjection.Services.GetService<ITransmittableProcessor>();
			handlerProvider = dependencyInjection.Services.GetService<IHandlerProvider>();
			eventReactor = dependencyInjection.Services.GetService<EventReactor>();
			messenger = dependencyInjection.Services.GetService<IMessenger>();
			identityProvider = dependencyInjection.Services.GetService<IIdentityProvider>();
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
				logger.Error(e.ToString());
				Console.WriteLine(e);
			}
		}
	}
}
