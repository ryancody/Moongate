using Moongate.Network.Events;
using Network;
using Network.Controllers;
using Network.Models;
using System;
using Telepathy;
using TelepathyClient = Telepathy.Client;

namespace Moongate.Network
{
	public class Client
	{
		public bool Connected { get => telepathyClient.Connected; }
		public bool Connecting { get => telepathyClient.Connecting; }

		private readonly TelepathyClient telepathyClient;
		private readonly ILogger logger;
		private readonly IMessageReceiver messageReceiver;
		private readonly IMessenger messenger;

		public static event EventHandler<PlayerConnectionArgs> ConnectedToServer;
		public static event EventHandler<PlayerConnectionArgs> DisconnectedFromServer;

		public Client (ILogger logger, TelepathyClient telepathyClient, IMessenger messenger, IMessageReceiver messageReceiver)
		{
			this.logger = logger;
			this.telepathyClient = telepathyClient;
			this.messenger = messenger;
			this.messageReceiver = messageReceiver;

			logger.Info($@"Client
			 - Time: {DateTime.Now}
			 - Name: {LocalId.Name}
			 - GUID: { LocalId.Guid}");
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
				while (telepathyClient.GetNextMessage(out var msg))
				{
					logger.Debug($@"Client received {msg.eventType} message");

					switch (msg.eventType)
					{
						case EventType.Connected:

							ConnectedToServer?.Invoke(this, null);
							break;

						case EventType.Data:

							messageReceiver.Receive(null, msg.data);
							break;

						case EventType.Disconnected:

							Disconnect();
							break;
					}
				}

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

		public void Transmit ()
		{
			messenger.TransmitQueue();
		}

		public void Receive (ITransmittable t)
		{
			messageReceiver.Receive(null, t);
		}
	}
}
