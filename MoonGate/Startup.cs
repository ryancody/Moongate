using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Moongate.Network;
using Network;
using Network.Utils;
using System;
using TelepathyClient = Telepathy.Client;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class Startup
	{
		public IServiceProvider ServiceProvider { get; set; }
		public IConfiguration Configuration { get; set; }

		public Startup ()
		{
			var configuration = new ConfigurationBuilder();

			configuration.Build();

			var serviceCollection = new ServiceCollection();

			//var playerStateController = new PlayerStateController(logger, new PlayerState());
			//serviceCollection.AddSingleton(playerStateController);

			//var gameStateController = new GameStateController(logger, new GameState());
			//serviceCollection.AddSingleton(gameStateController);

			//var serializer = new Serializer();
			//serviceCollection.AddSingleton(serializer);

			//var handlerFactory = new HandlerFactory(logger);
			//serviceCollection.AddSingleton(handlerFactory);

			//var transmittableFactory = new TransmittableFactory(logger);
			//serviceCollection.AddSingleton(transmittableFactory);

			//var messageReceiver = new MessageReceiver(logger, serializer);
			//serviceCollection.AddSingleton(messageReceiver);

			//var telepathyClient = new TelepathyClient();
			//serviceCollection.AddSingleton(telepathyClient);

			//var telepathyServer = new TelepathyServer();
			//serviceCollection.AddSingleton(telepathyServer);

			//var clientMessenger = new ClientMessenger(logger, telepathyClient, transmittableFactory, gameStateController, serializer);
			//serviceCollection.AddSingleton(clientMessenger);

			//var serverMessenger = new ServerMessenger(logger, serializer, telepathyServer, playerStateController, transmittableFactory, handlerFactory);
			//serviceCollection.AddSingleton(serverMessenger);

			//serviceCollection.AddSingleton(sp => new Client(logger, telepathyClient, clientMessenger, messageReceiver));

			//serviceCollection.AddSingleton(sp => new Server(logger, telepathyServer, serverMessenger, messageReceiver));

			//serviceCollection.AddSingleton(sp => new MessageProcessor(logger, messageReceiver, handlerFactory));

			ServiceProvider = serviceCollection.BuildServiceProvider();
		}

		public void ConfigureServices (IServiceCollection services)
		{
			var loggerConfig = new LoggerConfig();
			services.AddLogger(loggerConfig);

			services.AddStateController();
		}
	}
}
