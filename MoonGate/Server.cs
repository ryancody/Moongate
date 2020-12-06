﻿using Microsoft.Extensions.DependencyInjection;
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
			telepathyServer = services.ServiceProvider.GetRequiredService<TelepathyServer>();

			identityProvider.Id.IsServer = isServer;
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
