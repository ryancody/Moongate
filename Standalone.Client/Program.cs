using Moongate.State.Models;
using System;
using System.Collections.Generic;

namespace Moongate
{
	class Program
	{
		private static Client client;
		private static Dictionary<string, Func<string>> keyCommands;

		static void Main (string[] args)
		{
			client = new Client();
			keyCommands = new Dictionary<string, Func<string>>
			{
				["c"] = Connect,
				["d"] = Disconnect,
				["e"] = CreateEntity,
				["p"] = Ping,
				["q"] = Quit
			};

			var running = true;
			ConsoleKeyInfo consoleKeyInfo;

			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Console.WriteLine(version);

			Console.WriteLine($@"
			c - connect
			d - disconnect
			e - create entity
			w - player input, up
			p - ping server");

			while (running)
			{

				if (Console.KeyAvailable)
				{
					consoleKeyInfo = Console.ReadKey(true);

					if (keyCommands.TryGetValue(consoleKeyInfo.KeyChar.ToString().ToLower(), out var command))
					{
						var response = command();
						Console.WriteLine(response);
					}
					else
					{
						Console.WriteLine("Unknown command");
					}
				}

				client.Run();
			}
		}

		private static string Connect ()
		{
			client.Connect("localhost", 8888);
			return "Connected";
		}

		private static string Disconnect ()
		{
			client.Disconnect();
			return "Disconnected";
		}

		private static string CreateEntity ()
		{
			var entity = new Entity()
			{
				Guid = Guid.NewGuid().ToString(),
				Id = "test ID",
				Name = "test Name",
				Owner = client.Id.Guid
			};

			client.Farspeaker.Input.TransmitEntity(entity);

			return "Sent entity to server";
		}

		private static string Ping ()
		{
			client.Farspeaker.Input.Ping();
			return "";
		}

		private static string Quit ()
		{
			client.Farspeaker.Input.Ping();
			return "Quitting...";
		}
	}
}
