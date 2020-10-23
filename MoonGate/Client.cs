﻿using CoreNET.Controllers.MessageProcessor;
using Moongate.Network.Events;
using Network;
using Network.Controllers;
using Network.Factory;
using Network.Models;
using Network.Utils;
using System;
using Telepathy;
using Logger = Network.Utils.Logger;

namespace Moongate.Network
{
	public class Client
	{
		public bool Connected { get { return client.Connected; } }
		public bool Connecting { get { return client.Connecting; } }
		public EventPublisherBase EventPublisher;
		public EventListener EventListener;

		readonly Telepathy.Client client = new Telepathy.Client();
		readonly ILogger logger;
		readonly IMessageReceiver messageReceiver;
		readonly IMessageProcessor messageProcessor;
		readonly IStateController stateController;
		readonly IMessenger messenger;
		readonly ITransmittableFactory transmittableFactory;
		readonly ISerializer serializer;
		readonly IHandlerFactory handlerFactory;
		readonly PlayerStateController playerStateController;
		readonly GameStateController gameStateController;

		public static event EventHandler<PlayerConnectionArgs> ConnectedToServer;
		public static event EventHandler<PlayerConnectionArgs> DisconnectedFromServer;

		public Client ()
		{
			var playerState = new PlayerState();
			var gameState = new GameState();

			logger = new Logger("CLIENT", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });

			playerStateController = new PlayerStateController(logger, playerState);
			gameStateController = new GameStateController(logger, gameState);

			serializer = new Serializer();
			handlerFactory = new HandlerFactory(logger);
			transmittableFactory = new TransmittableFactory(logger);
			messageReceiver = new MessageReceiver(logger, serializer);
			messageProcessor = new MessageProcessor(logger, messageReceiver, handlerFactory);
			messenger = new ClientMessenger(logger, client, transmittableFactory, gameStateController, serializer);
			stateController = new AgentStateController(logger, playerStateController, gameStateController, handlerFactory, messenger, transmittableFactory);

			EventPublisher = new EventPublisher(logger, handlerFactory, gameStateController);
			EventListener = new EventListener(logger, transmittableFactory, messenger, gameStateController);

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

							messageReceiver.Receive(null, msg.data);
							break;

						case EventType.Disconnected:

							Disconnect();
							break;
					}
				}

				messenger.TransmitQueue();
			}
			catch (Exception e)
			{
				logger.Error(e.ToString());
				Console.WriteLine(e);
			}
		}

		public void Ping ()
		{
			((ClientMessenger)messenger).Ping();
		}

		public void Disconnect ()
		{
			DisconnectedFromServer?.Invoke(this, null);
			client.Disconnect();
		}

		public void QueueTransmission (ITransmittable t)
		{
			messenger.QueueTransmission(t);
		}

		public void Transmit ()
		{
			messenger.TransmitQueue();
		}

		public void Receive (ITransmittable t)
		{
			messageReceiver.Receive(null, t);
		}
	}
}
