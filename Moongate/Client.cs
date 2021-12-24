using Microsoft.Extensions.DependencyInjection;
using Moongate.IO;
using Moongate.Models.Identity;
using Telepathy;
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
			telepathyClient = (TelepathyClient)dependencyInjection.ServiceProvider.GetRequiredService<Common>();
			Farspeaker = dependencyInjection.ServiceProvider.GetRequiredService<Farspeaker>();

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
	}
}
