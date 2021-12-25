using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Transmittable.Factory;
using System;

namespace Moongate.Events.Reactor.EventHandlers
{
	public class ClientMessageListenerEventHandler : IEventHandler
	{
		private readonly ILogger<ClientMessageListenerEventHandler> logger;
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly IIdentityProvider identityProvider;

		public ClientMessageListenerEventHandler(ILogger<ClientMessageListenerEventHandler> logger, 
			IMessageListener messageListener, 
			IMessenger messenger, 
			ITransmittableFactory transmittableFactory, 
			IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.messenger = messenger;
			this.transmittableFactory = transmittableFactory;
			this.identityProvider = identityProvider;

			messageListener.Connected += MessageListener_Connected;
			messageListener.Disconnected += MessageListener_Disconnected;
		}

		private void MessageListener_Connected (object sender, MessageArgs e)
		{
			Console.WriteLine($"connected to server");
			logger.LogInformation($"connected to server");

			var playerHandshakeArgs = new ClientArgs
			{
				Name = identityProvider.Id.Name,
				Guid = identityProvider.Id.Guid
			};
			var transmission = transmittableFactory.Build(TransmissionType.PlayerHandshake, playerHandshakeArgs);

			messenger.QueueTransmission(transmission);
		}

		private void MessageListener_Disconnected (object sender, MessageArgs e)
		{
			Console.WriteLine("disconnected from server");
			logger.LogInformation("disconnected from server");
		}
	}
}
