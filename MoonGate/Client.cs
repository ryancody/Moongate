using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Messenger;
using Moongate.Models;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;
using TelepathyClient = Telepathy.Client;

namespace Moongate.Network
{
	public class Client
	{
		public bool Connected { get => telepathyClient.Connected; }
		public bool Connecting { get => telepathyClient.Connecting; }

		private readonly ILogger logger;
		private readonly TelepathyClient telepathyClient;
		private readonly IMessageListener messageListener;
		private readonly IMessenger messenger;

		public static event EventHandler<PlayerConnectionArgs> ConnectedToServer;
		public static event EventHandler<PlayerConnectionArgs> DisconnectedFromServer;

		public Client (ILogger logger, TelepathyClient telepathyClient, IMessageListener messageListener, IMessenger messenger)
		{
			this.logger = logger;
			this.telepathyClient = telepathyClient;
			this.messageListener = messageListener;
			this.messenger = messenger;

			logger.Info($@"Client
			 - Time: {DateTime.Now}
			 - Name: {LocalId.Name}
			 - GUID: {LocalId.Guid}");
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
