using CoreSDK.Controllers;
using CoreSDK.Factory;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using Telepathy;

namespace CoreSDK
{
	public class CoreClient
	{
		public bool Connected { get { return client.Connected; } }
		public bool Connecting { get { return client.Connecting; } }
		
		readonly Client client = new Client();
		readonly ILogger logger;
		readonly IMessageProcessor messageProcessor;
		readonly IStateController stateController;
		readonly IClientMessenger messenger;
		readonly ITransmittableFactory transmittableFactory;
		readonly ISerializer serializer;
		readonly IHandlerFactory handlerFactory;

		public static event EventHandler ConnectedToServer;
		public static event EventHandler DisconnectedFromServer;
		
		public CoreClient ()
		{
			serializer = new Serializer();
			logger = new Utils.Logger("CLIENT", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });
			handlerFactory = new HandlerFactory(logger);
			stateController = new ClientStateController(logger);
			transmittableFactory = new TransmittableFactory(logger);
			messageProcessor = new MessageProcessor(logger, serializer, handlerFactory);
			messenger = new ClientMessenger(logger, client, transmittableFactory, serializer);

			logger.Info($@"Client
			 - Time: {DateTime.Now}
			 - Name: {LocalId.Name}
			 - GUID: { LocalId.Guid}");
		}

		public void Connect (string host, int port)
		{
			Console.WriteLine("CLIENT - Hi, I'm " + LocalId.Name);

			// create and start the client
			client.Connect(host, port);
			logger.Info("Client connected");
		}

		public void Run ()
		{
			try
			{
				// grab all new messages. do this in your Update loop.
				Message msg;
				while (client.GetNextMessage(out msg))
				{
					logger.Debug($@"Client received {msg.eventType} message");

					switch (msg.eventType)
					{
						case EventType.Connected:

							ConnectedToServer?.Invoke(this, null);
							break;

						case EventType.Data:

							messageProcessor.Receive(msg.data);
							break;

						case EventType.Disconnected:

							Disconnect();
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

		public void Ping ()
		{
			messenger.Ping();
		}

		public void Disconnect ()
		{
			DisconnectedFromServer?.Invoke(this, null);
			client.Disconnect();
		}

		public void Transmit (ITransmittable t)
		{
			messenger.Transmit(t);
		}

		public void Receive (ITransmittable t)
		{
			messageProcessor.Receive(t);
		}
	}
}
