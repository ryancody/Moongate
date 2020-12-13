using DependencyInjection;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.IO;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.State.Controller;
using Moongate.State.Models;
using Moongate.Transmittable.Factory;
using Moongate.Transmittable.Processor;
using Moongate.Utils;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class DependencyInjection
	{
		public Services Services = new Services();

		public DependencyInjection (bool isServer)
		{
			ConfigureServices(isServer);
		}

		private void ConfigureServices (bool isServer)
		{
			Services.AddService<IIdentityProvider>(new IdentityProvider());
			var identityProvider = Services.GetService<IIdentityProvider>();
			identityProvider.Id.IsServer = isServer;

			var clientServer = isServer ? "server" : "client";

			Services.AddService<ILogger>(new Logger.Logger(clientServer, new LoggerLevel[] { LoggerLevel.Debug }, identityProvider.Id.Guid));
			var logger = Services.GetService<ILogger>();

			Services.AddService(new Serializer());
			var serializer = Services.GetService<Serializer>();

			Services.AddService<ITransmittableFactory>(new TransmittableFactory(logger, identityProvider));
			var transmittableFactory = Services.GetService<ITransmittableFactory>();

			Services.AddService(new PlayerStateController(logger, new PlayerState()));
			var playerStateController = Services.GetService<PlayerStateController>();

			Services.AddService<IHandlerProvider>(new HandlerProvider(logger));
			var handlerProvider = Services.GetService<IHandlerProvider>();

			TelepathyClient client;
			TelepathyServer server;
			IMessenger messenger;
			IMessageListener messageListener;

			if (isServer)
			{
				Services.AddService(new TelepathyServer());
				server = Services.GetService<TelepathyServer>();

				Services.AddService<IMessenger>(new ServerMessenger(logger, serializer, server, playerStateController));
				messenger = Services.GetService<IMessenger>();

				Services.AddService<IMessageListener>(new MessageListener(logger, server));
				messageListener = Services.GetService<IMessageListener>();
			}
			else
			{
				Services.AddService(new TelepathyClient());
				client = Services.GetService<TelepathyClient>();

				Services.AddService<IMessenger>(new ClientMessenger(logger, serializer, client));
				messenger = Services.GetService<IMessenger>();

				Services.AddService(new Farspeaker(logger, transmittableFactory, messenger, identityProvider, handlerProvider));

				Services.AddService<IMessageListener>(new MessageListener(logger, client));
			}

			messageListener = Services.GetService<IMessageListener>();

			Services.AddService<IMessageReceiver>(new MessageReceiver(logger, serializer, messageListener));
			var messageReceiver = Services.GetService<IMessageReceiver>();

			Services.AddService<ITransmittableProcessor>(new TransmittableProcessor(logger, messageReceiver, handlerProvider, identityProvider));

			Services.AddService(new GameStateController(logger, new GameState(), handlerProvider));
			var gameStateController = Services.GetService<GameStateController>();

			Services.AddService(new EventReactor(logger, handlerProvider, gameStateController, playerStateController, transmittableFactory, messageListener, messenger, identityProvider));
		}
	}
}
