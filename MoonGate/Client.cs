using Microsoft.Extensions.DependencyInjection;
using Moongate.Identity.Provider;
using Moongate.IO;
using Moongate.Logger;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Listener;
using Moongate.Models.Identity;
using Moongate.Models.Transmittable;
using System;
using TelepathyClient = Telepathy.Client;

namespace Moongate
{
	public class Client
	{
		public bool Connected { get => telepathyClient.Connected; }
		public bool Connecting { get => telepathyClient.Connecting; }
		public Id Id { get { return identityProvider.Id; } }
		public Farspeaker Farspeaker { get; set; }

		private readonly ILogger logger;
		private readonly TelepathyClient telepathyClient;
		private readonly IMessageListener messageListener;
		private readonly IMessenger messenger;
		private readonly IIdentityProvider identityProvider;

		private readonly DependencyInjection services;

		public Client ()
		{
			services = new DependencyInjection(false);
			logger = services.ServiceProvider.GetService<ILogger>();
			telepathyClient = services.ServiceProvider.GetRequiredService<TelepathyClient>();
			messageListener = services.ServiceProvider.GetRequiredService<IMessageListener>();
			messenger = services.ServiceProvider.GetRequiredService<IMessenger>();
			identityProvider = services.ServiceProvider.GetRequiredService<IIdentityProvider>();
		}

		public void Connect (string host, int port)
		{
			telepathyClient.Connect(host, port);
			logger.Info("Client connected");
		}

		public void Disconnect ()
		{
			telepathyClient.Disconnect();
			logger.Info("Client disconnected");
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

		public void QueueTransmission (ITransmittable t)
		{
			messenger.QueueTransmission(t);
		}
	}
}
