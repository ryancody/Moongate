using Moongate.IO;
using Moongate.Models.Identity;
using System;
using TelepathyClient = Telepathy.Client;

namespace Moongate
{
	public class Client : Agent
	{
		public bool Connected { get => telepathyClient.Connected; }
		public bool Connecting { get => telepathyClient.Connecting; }
		public Id Id { get { return identityProvider.Id; } }
		public Farspeaker Farspeaker { get; set; }

		private const bool isServer = false;
		private readonly TelepathyClient telepathyClient;

		public Client () : base(isServer)
		{
			telepathyClient = dependencyInjection.Services.GetService<TelepathyClient>();
			Farspeaker = dependencyInjection.Services.GetService<Farspeaker>();

			identityProvider.Id.IsServer = isServer;
		}

		public void Connect (string host, int port)
		{
			telepathyClient.Connect(host, port);
		}

		public void Disconnect ()
		{
			telepathyClient.Disconnect();
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
	}
}
