using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messenger;
using Moongate.Models;
using Moongate.Models.Events;
using System;
using TelepathyServer = Telepathy.Server;

namespace Network
{
	public class Server
	{
		readonly TelepathyServer telepathyServer;
		readonly IMessageListener messageListener;
		readonly IMessenger messenger;
		readonly ILogger logger;

		public static event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public static event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public bool Active { get => telepathyServer.Active; }

		public Server (ILogger logger, TelepathyServer telepathyServer, IMessenger messenger, IMessageListener messageListener)
		{
			this.telepathyServer = telepathyServer;
			this.logger = logger;
			this.messenger = messenger;
			this.messageListener = messageListener;
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
				messageListener.Listen();

				messenger.TransmitQueue();
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
