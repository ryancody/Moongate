using System;
using TelepathyServer = Telepathy.Server;

namespace Moongate
{
	public class Server : Agent
	{
		private readonly TelepathyServer telepathyServer;
		public bool Active { get => telepathyServer.Active; }

		private const bool isServer = true;

		public Server () : base(isServer)
		{
			telepathyServer = dependencyInjection.Services.GetService<TelepathyServer>();

			identityProvider.Id.IsServer = isServer;
		}

		public void Start (int port)
		{
			telepathyServer.Start(port);
		}

		public void Stop ()
		{
			Console.WriteLine("Closing Server...");
			telepathyServer.Stop();
		}
	}
}
