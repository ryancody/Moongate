using Microsoft.Extensions.DependencyInjection;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.Transmittable.Processor;

namespace Moongate
{
	public class Agent
	{
		protected readonly DependencyInjection services;
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
			services = new DependencyInjection(isServer);
			logger = services.ServiceProvider.GetService<ILogger>();
			messageListener = services.ServiceProvider.GetRequiredService<IMessageListener>();
			messageReceiver = services.ServiceProvider.GetRequiredService<IMessageReceiver>();
			transmittableProcessor = services.ServiceProvider.GetRequiredService<ITransmittableProcessor>();
			handlerProvider = services.ServiceProvider.GetRequiredService<IHandlerProvider>();
			eventReactor = services.ServiceProvider.GetRequiredService<EventReactor>();
			messenger = services.ServiceProvider.GetRequiredService<IMessenger>();
			identityProvider = services.ServiceProvider.GetRequiredService<IIdentityProvider>();

		}
	}
}
