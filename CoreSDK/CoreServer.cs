using CoreSDK.Controllers;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telepathy;

namespace CoreSDK
{
	public class CoreServer
	{
		readonly Server server;
		readonly PlayerManager playerManager;
		readonly GameState gameState;
		readonly IMessageProcessor messageProcessor;
		readonly IStateController serverStateController;
		readonly IMessenger serverMessenger;
		readonly ILogger logger;

		public static event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public static event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public bool Active { get; private set; }


		public CoreServer ()
		{
			server = new Server();
			playerManager = new PlayerManager();
			gameState = new GameState();
			messageProcessor = new MessageProcessor(logger);
			serverStateController = new ServerStateController(logger, playerManager, gameState);
			serverMessenger = new ServerMessenger(logger, server, playerManager);
			logger = new Utils.Logger("SERVER", new LoggerLevel[] { LoggerLevel.Info, LoggerLevel.Error });
		}

		public void Start (int port)
		{
			PlayerConnected += new EventHandler<PlayerConnectionArgs>(OnPlayerConnected);
			PlayerDisconnected += new EventHandler<PlayerConnectionArgs>(OnPlayerDisconnected);
			PlayerHandshakeHandler.PlayerHandshaked += OnPlayerHandshake;
			ServerStateController.GameStateRequested += OnGameStateRequested;

			Console.WriteLine("SERVER - Hi, I'm " + LocalId.Name);
			logger.Debug(@"Server
			 - Time: {DateTime.Now}
			 - Instance Name: {LocalId.Name} - GUID: {LocalId.Guid}");

			// create and start the server
			server.Start(port);
			Active = true;
		}

		private void OnGameStateRequested (object sender)
		{
			var t = new Transmission(MessageType.GameStateRequest, e.Payload);
			server.Send(e.ConnectionId, t.Serialized());
		}

		public void Run ()
		{
			try
			{
				// grab all new messages. do this in your Update loop.
				Message msg;
				while (server.GetNextMessage(out msg))
				{
					switch (msg.eventType)
					{
						case EventType.Connected:
							logger.Debug("Server received EventType.Connected message");

							var connectArgs = new BasicPlayerRequestArgs()
							{
								ConnectionId = msg.connectionId
							};

							Console.WriteLine(msg.connectionId + " Connected");

							PlayerConnected?.Invoke(this, connectArgs);
							break;

						case EventType.Data:
							logger.Debug("Server received EventType.Data message");

							messageProcessor.Receive(msg.connectionId, msg.data);
							break;

						case EventType.Disconnected:
							logger.Debug("Server received EventType.Disconnected message");

							var player = playerManager.GetPlayer(msg.connectionId);
							var disconnectArgs = new BasicPlayerRequestArgs()
							{
								ConnectionId = player.ConnectionId,
								PlayerName = player.Name,
								ClientGuid = player.GUID
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

		protected virtual void OnPlayerConnected (object sender, BasicPlayerRequestArgs e)
		{
			logger.Debug("Server invoked OnPlayerConnected");

			Console.WriteLine("Player Connected.  Connection ID: " + e.ConnectionId);
		}
	}
}
