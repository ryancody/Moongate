using System;
using System.Collections.Generic;

namespace Moongate.App
{
	public class ServerCommands
	{
		private readonly Server server;

		public Dictionary<string, Action> KeyCommands { get; }

		public ServerCommands (Agent agent)
		{
			if (agent is Server server)
			{
				this.server = server;
			}
			else
			{
				throw new IncorrectAgentError("Agent was not Server as expected");
			}

			KeyCommands = new Dictionary<string, Action>
			{
				["d"] = Disconnect
			};
		}

		private void Disconnect ()
		{
			Console.WriteLine("Disconnecting...");
			server.Stop();
		}
	}
}
