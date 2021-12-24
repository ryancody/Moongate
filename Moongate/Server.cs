using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Telepathy;
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
			telepathyServer = (TelepathyServer)dependencyInjection.ServiceProvider.GetRequiredService<Common>();

			identityProvider.Id.IsServer = isServer;
		}

		public void Start (int port)
		{
			logger.LogInformation($@"Server
			 - Time: {DateTime.Now}
			 - Instance Name: {identityProvider.Id?.Name}
			 - GUID: {identityProvider.Id?.Guid}");

			telepathyServer.Start(port);
		}

		public void Stop ()
		{
			Console.WriteLine("Closing Server...");
			telepathyServer.Stop();
		}
	}
}
