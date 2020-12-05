using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.IO;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
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

		private readonly DependencyInjection services;

		public Farspeaker Farspeaker { get; set; }

		public bool Active { get => telepathyServer.Active; }

		public Server ()
		{
			services = new DependencyInjection(true);
			logger = services.ServiceProvider.GetService<ILogger>();
			telepathyServer = services.ServiceProvider.GetRequiredService<TelepathyServer>();
			messenger = services.ServiceProvider.GetRequiredService<IMessenger>();
			messageListener = services.ServiceProvider.GetRequiredService<IMessageListener>();
			identityProvider = services.ServiceProvider.GetRequiredService<IIdentityProvider>();
		}

		public void Start (int port)
		{
			logger.Info($@"Server
			 - Time: {DateTime.Now}
			 - Instance Name: {identityProvider.Id?.Name}
			 - GUID: {identityProvider.Id?.Guid}");

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
