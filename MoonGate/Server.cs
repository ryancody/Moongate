using Moongate.Network.Events;
using Network.Controllers;
using System;
using Telepathy;
using TelepathyServer = Telepathy.Server;

namespace Network
{
	public class Server
	{
		readonly TelepathyServer telepathyServer;
		readonly IMessageReceiver messageReceiver;
		readonly IMessenger serverMessenger;
		readonly ILogger logger;

		public static event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public static event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public bool Active { get => telepathyServer.Active; }

		public Server (ILogger logger, TelepathyServer telepathyServer, IMessenger messenger, IMessageReceiver messageReceiver)
		{
			this.telepathyServer = telepathyServer;
			this.logger = logger;
			this.serverMessenger = messenger;
			this.messageReceiver = messageReceiver;
		}

		public void Start (int port)
		{
			Console.WriteLine("SERVER - Hi, I'm " + LocalId.Name);

			logger.Info($@"Server
			 - Time: {DateTime.Now}
			 - Instance Name: {LocalId.Name}
			 - GUID: {LocalId.Guid}");

			telepathyServer.Start(port);
		}

		public void Run ()
		{
			try
			{
				while (telepathyServer.GetNextMessage(out var msg))
				{
					logger.Debug($@"Server received {msg.eventType} message from {msg.connectionId}");

					switch (msg.eventType)
					{
						case EventType.Connected:
							logger.Debug($@"{telepathyServer.GetClientAddress(msg.connectionId)} connected on {msg.connectionId}");

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
			telepathyServer.Stop();
		}
	}
}
