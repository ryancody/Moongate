using Moongate.State.Models;
using System;
using System.Collections.Generic;

namespace Moongate.App
{
	public class ClientCommands
	{
		private readonly Client client;
		private readonly string host;
		private readonly int port;

		public Dictionary<string, Action> KeyCommands { get; }

		public ClientCommands (Agent agent, string host, int port)
		{
			if (agent is Client client)
			{
				this.client = client;
				this.host = host;
				this.port = port;
			}
			else
			{
				throw new IncorrectAgentError("Agent was not Client as expected");
			}

			KeyCommands = new Dictionary<string, Action>
			{
				["c"] = Connect,
				["d"] = Disconnect,
				["e"] = CreateEntity,
				["p"] = Ping
			};
		}

		private void Connect ()
		{
			Console.WriteLine("Connecting...");
			client.Connect(host, port);
		}

		private void Disconnect ()
		{
			Console.WriteLine("Disconnecting...");
			client.Disconnect();
		}

		private void CreateEntity ()
		{
			var entity = new Entity()
			{
				Guid = Guid.NewGuid().ToString(),
				Id = "test ID",
				Name = "test Name",
				Owner = client.Id.Guid
			};

			client.Farspeaker.Input.TransmitEntity(entity);
		}

		private void Ping ()
		{
			client.Farspeaker.Input.Ping();
		}
	}
}
