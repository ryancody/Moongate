using CoreSDK.Controllers;
using CoreSDK.Factory;
using CoreSDK.Utils;
using System;
using Telepathy;

namespace CoreSDK
{
	public class CoreServer
	{
		readonly Server server;
		readonly IMessageProcessor messageProcessor;
		readonly IStateController serverStateController;
		readonly IServerMessenger serverMessenger;
		readonly ILogger logger;
		readonly ISerializer serializer;
		readonly ITransmittableFactory transmittableFactory;
		readonly IHandlerFactory handlerFactory;

		public static event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public static event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public bool Active { get; private set; }

		public CoreServer ()
		{
			
			server = new Server();
			logger = new Utils.Logger("SERVER", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });
			serializer = new Serializer();
			transmittableFactory = new TransmittableFactory(logger);

			var playerState = new PlayerState();
			var gameState = new GameState();

			var playerStateController = new PlayerStateController(logger, playerState);
			var gameStateController = new GameStateController(logger, gameState);

			handlerFactory = new HandlerFactory(logger);
			messageProcessor = new MessageProcessor(logger, serializer, handlerFactory);
			serverMessenger = new ServerMessenger(logger, server, playerStateController, transmittableFactory, handlerFactory, serializer);
			serverStateController = new ServerStateController(logger, playerStateController, gameStateController, handlerFactory, serverMessenger);
		}

		public void Start (int port)
		{
			Console.WriteLine("SERVER - Hi, I'm " + LocalId.Name);

			logger.Info(@"Server
			 - Time: {DateTime.Now}
			 - Instance Name: {LocalId.Name}
			 - GUID: {LocalId.Guid}");

			// create and start the server
			server.Start(port);
			Active = true;
		}

		public void Run ()
		{
			try
			{
				// grab all new messages. do this in your Update loop.
				Message msg;
				while (server.GetNextMessage(out msg))
				{
					logger.Debug($@"Server received {msg.eventType} message");
										
					switch (msg.eventType)
					{
						case EventType.Connected:
							var connectArgs = new PlayerConnectionArgs()
							{
								ConnectionId = msg.connectionId
							};

							PlayerConnected?.Invoke(this, connectArgs);
							break;

						case EventType.Data:

							messageProcessor.Receive(msg.connectionId, msg.data);
							break;

						case EventType.Disconnected:
							var disconnectArgs = new PlayerConnectionArgs()
							{
								ConnectionId = msg.connectionId
							};

							PlayerDisconnected?.Invoke(this, disconnectArgs);
							break;
					}
				}

				messageProcessor.Process();
			}
			catch (Exception e)
			{
				logger.Error(e.ToString());
				Console.WriteLine(e);
			}
		}

		public void Stop ()
		{
			Console.WriteLine("Closing Server...");
			server.Stop();
			Active = false;
		}
	}
}
