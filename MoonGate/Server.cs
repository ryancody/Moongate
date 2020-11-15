using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using System;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class Server
	{
		private readonly TelepathyServer telepathyServer;
		private readonly IMessageListener messageListener;
		private readonly IMessenger messenger;
		private readonly ILogger logger;
		private readonly IIdentityProvider identityProvider;

		public static event EventHandler<PlayerConnectionArgs> PlayerConnected;
		public static event EventHandler<PlayerConnectionArgs> PlayerDisconnected;

		public bool Active { get => telepathyServer.Active; }

		public Server (ILogger logger, 
						TelepathyServer telepathyServer, 
						IMessenger messenger, 
						IMessageListener messageListener,
						IIdentityProvider identityProvider)
		{
			this.telepathyServer = telepathyServer;
			this.logger = logger;
			this.messenger = messenger;
			this.messageListener = messageListener;
			this.identityProvider = identityProvider;
		}

		public void Start (int port)
		{
			Console.WriteLine("SERVER - Hi, I'm " + identityProvider.Id.Name);

			logger.Info($@"Server
			 - Time: {DateTime.Now}
			 - Instance Name: {identityProvider.Id.Name}
			 - GUID: {identityProvider.Id.Guid}");

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
