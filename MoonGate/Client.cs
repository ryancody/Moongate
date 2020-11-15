using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
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

		private readonly ILogger logger;
		private readonly TelepathyClient telepathyClient;
		private readonly IMessageListener messageListener;
		private readonly IMessenger messenger;
		private readonly IIdentityProvider identityProvider;

		public static event EventHandler<PlayerConnectionArgs> ConnectedToServer;
		public static event EventHandler<PlayerConnectionArgs> DisconnectedFromServer;

		public Client (ILogger logger,
						TelepathyClient telepathyClient,
						IMessageListener messageListener,
						IMessenger messenger,
						IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.telepathyClient = telepathyClient;
			this.messageListener = messageListener;
			this.messenger = messenger;

			logger.Info($@"Client
			 - Time: {DateTime.Now}
			 - Name: {identityProvider.Id.Name}
			 - GUID: {identityProvider.Id.Guid}");
		}

		public void Connect (string host, int port)
		{
			telepathyClient.Connect(host, port);
			logger.Info("Client connected");
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

		public void Ping ()
		{
			((ClientMessenger)messenger).Ping();
		}

		public void Disconnect ()
		{
			DisconnectedFromServer?.Invoke(this, null);
			telepathyClient.Disconnect();
		}

		public void QueueTransmission (ITransmittable t)
		{
			messenger.QueueTransmission(t);
		}
	}
}
