using CoreSDK.Controllers;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using Telepathy;

namespace CoreSDK
{
	public class CoreClient
	{
		readonly Client client = new Client();
		readonly IMessageProcessor messageProcessor;
		readonly ILogger logger;
		readonly IStateController stateController;
		readonly ClientMessenger messenger;

		public static event EventHandler ConnectedToServer;
		public static event EventHandler DisconnectedFromServer;

		public bool Connected { get { return client.Connected; } }
		public bool Connecting { get { return client.Connecting; } }
		
		public CoreClient ()
		{
			logger = new Utils.Logger("CLIENT", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });
			messageProcessor = new MessageProcessor(logger);
			stateController = new ClientStateController(logger);
			messenger = new ClientMessenger(logger, client);

			logger.Info($@"Client
			 - Time: {DateTime.Now}
			 - Name: {LocalId.Name}
			 - GUID: { LocalId.Guid}");

			DisconnectedFromServer += OnDisconnectedFromServer;
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

							DisconnectedFromServer?.Invoke(this, null);
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
			client.Disconnect();
		}

		public void Transmit (Transmission t)
		{
			messenger.Transmit(t);
		}

		protected void OnDisconnectedFromServer (object sender, EventArgs a)
		{
			Disconnect();
		}
	}
}
