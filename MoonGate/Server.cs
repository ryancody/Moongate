using CoreNET.Controllers.MessageProcessor;
using Network.Controllers;
using Network.Factory;
using Network.Utils;
using System;
using Telepathy;

namespace Network
{
	public class Server
	{
		readonly Telepathy.Server server;
		readonly IMessageReceiver messageReceiver;
		readonly IMessageProcessor messageProcessor;
		readonly IStateController serverStateController;
		readonly IMessenger serverMessenger;
		readonly ILogger logger;
		readonly ISerializer serializer;
		readonly ITransmittableFactory transmittableFactory;
		readonly IHandlerFactory handlerFactory;

		public static event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public static event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public bool Active { get; private set; }

		public Server ()
		{
			server = new Telepathy.Server();
			logger = new Utils.Logger("SERVER", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });
			serializer = new Serializer();
			transmittableFactory = new TransmittableFactory(logger);

			var playerState = new PlayerState();
			var gameState = new GameState();

			var playerStateController = new PlayerStateController(logger, playerState);
			var gameStateController = new GameStateController(logger, gameState);

			handlerFactory = new HandlerFactory(logger);
			messageReceiver = new MessageReceiver(logger, serializer);
			messageProcessor = new MessageProcessor(logger, messageReceiver, handlerFactory);
			serverMessenger = new ServerMessenger(logger, server, playerStateController, messageReceiver, transmittableFactory, handlerFactory, serializer);
			serverStateController = new AgentStateController(logger, playerStateController, gameStateController, handlerFactory, serverMessenger, transmittableFactory);
		}

		public void Start (int port)
		{
			Console.WriteLine("SERVER - Hi, I'm " + LocalId.Name);

			logger.Info($@"Server
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
				Message msg;
				while (server.GetNextMessage(out msg))
				{
					logger.Debug($@"Server received {msg.eventType} message from {msg.connectionId}");

					switch (msg.eventType)
					{
						case EventType.Connected:
							logger.Debug($@"{server.GetClientAddress(msg.connectionId)} connected on {msg.connectionId}");

							break;

						case EventType.Data:

							messageReceiver.Receive(msg.connectionId, msg.data);
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

				serverMessenger.TransmitQueue();
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
