using Moongate.Models.Events;
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
				["p"] = Ping,
				["l"] = ListPlayers,
				["n"] = SendNetEvent,
				["r"] = RapidFireNetEvents
			};
		}

		private void SendNetEvent ()
		{
			Console.WriteLine("sending net event...");
			client.Farspeaker.Input.SendNetEvent(null);
		}

		private void RapidFireNetEvents ()
		{
			for (int i = 0; i < 3; i++)
			{
				var p = new { Sup = "nup", Nup = "Sup", Mup = "blah blah blah blah blah blah" };

				var e = new NetEventArgs
				{
					EventType = "test",
					SenderGuid = Guid.NewGuid().ToString(),
					Payload = p
				};
				Console.WriteLine("sending net event...");
				client.Farspeaker.Input.SendNetEvent(null);
			}
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

		private void Ping ()
		{
			client.Farspeaker.Input.Ping();
		}

		private void ListPlayers ()
		{
			Console.WriteLine("Players Connected");
			client.GetConnectedPlayers.ForEach(p => Console.WriteLine($"\n - {p.Name}"));
		}
	}
}
