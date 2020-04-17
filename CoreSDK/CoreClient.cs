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

		public static event EventHandler<PlayerConnectionArgs> ConnectedToServer;
		public static event EventHandler<PlayerConnectionArgs> DisconnectedFromServer;

		public bool Connected { get { return client.Connected; } }
		public bool Connecting { get { return client.Connecting; } }
		
		public CoreClient ()
		{
			logger = new Utils.Logger("CLIENT", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });
			messageProcessor = new MessageProcessor(logger);
			stateController = new ClientStateController(logger);
			messenger = new ClientMessenger(logger, client);
			
			logger.Info("Client");
			logger.Info(" - Time: " + DateTime.Now);
			logger.Info(" - Instance Name: " + LocalId.Name + " - GUID: " + LocalId.Guid);

			DisconnectedFromServer += new EventHandler<PlayerConnectionArgs>(OnDisconnectedFromServer);
		}

		public void Connect (string host, int port)
		{
			Console.WriteLine("CLIENT - Hi, I'm " + LocalId.Name);

			// create and start the client
			client.Connect(host, port);
			logger.Info("Client connected");

			RequestPlayersListHandler.PlayersListRequest += OnPlayersListReceived;
		}

		public void Run ()
		{
			try
			{
				// grab all new messages. do this in your Update loop.
				Message msg;
				while (client.GetNextMessage(out msg))
				{
					switch (msg.eventType)
					{
						case EventType.Connected:
							logger.Debug("Client received EventType.Connected message");

							var connectArgs = new BasicPlayerRequestArgs()
							{
								ConnectionId = msg.connectionId
							};

							ConnectedToServer?.Invoke(this, connectArgs);
							break;

						case EventType.Data:
							logger.Debug("Client received EventType.Data message");

							messageProcessor.Receive(msg.data);
							break;

						case EventType.Disconnected:
							logger.Debug("Client received EventType.Disconnected message");

							var disconnectArgs = new BasicPlayerRequestArgs()
							{
								ConnectionId = msg.connectionId
							};

							DisconnectedFromServer?.Invoke(this, disconnectArgs);
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

		public void Send (Transmission t)
		{
			messenger.Send(t);
		}

		public void Disconnect ()
		{
			client.Disconnect();
		}

		protected void OnDisconnectedFromServer (object sender, BasicPlayerRequestArgs a)
		{
			client.Disconnect();
		}

		protected void OnPlayersListReceived (object sender, BasicPlayerRequestArgs a)
		{
			try
			{
				Console.WriteLine("PlayerList");
				((List<Player>)a.Payload).ForEach(p => Console.WriteLine(p.ConnectionId + " " + p.Name + " " + p.GUID));
			}
			catch (Exception e)
			{ 
				Console.WriteLine(e); 
			}
		}
	}
}
