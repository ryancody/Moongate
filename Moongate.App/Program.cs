using System;
using System.Collections.Generic;
using System.Linq;

namespace Moongate.App
{
	class Program
	{
		private static Agent agent;
		private static bool isRunning = true;
		private static Dictionary<string, Action> keyCommands;

		private static string host = "localhost";
		private static int port = 8888;
		private static bool validPort;

		static void Main (string[] args)
		{
			Setup();

			Run();
		}

		private static void Setup ()
		{
			bool validChoice = false;

			while (!validChoice)
			{
				Console.WriteLine("Is this a [s]erver or a [c]lient?");
				var clientServerChoice = Console.ReadKey();

				Console.WriteLine("\n");

				if (clientServerChoice.Key == ConsoleKey.S)
				{
					SetupServer();
					validChoice = true;
				}
				else if (clientServerChoice.Key == ConsoleKey.C)
				{
					SetupClient();
					validChoice = true;
				}
				else if (clientServerChoice.Key == ConsoleKey.Escape)
				{
					isRunning = false;
					validChoice = true;
				}
				else
				{
					Console.WriteLine("Invalid input, try again or press ESC to quit");
				}
			}
		}

		private static void GetHost ()
		{
			Console.WriteLine($"Enter Host Name [{host}]:");
			var input = Console.ReadLine();

			if (input != "")
			{
				host = input;
			}
		}

		private static void GetPort ()
		{
			validPort = false;

			while (!validPort)
			{ 
				Console.WriteLine($"Enter Port [{port}]:");
				var input = Console.ReadLine();

				if (input == "")
				{
					input = port.ToString();
				}

				validPort = int.TryParse(input, out var result);
				port = result;

				if (!validPort)
				{
					Console.WriteLine("Port number must be an integer");
				}
			}
		}

		private static void SetupServer ()
		{
			Console.WriteLine("Creating server...");
			agent = new Server();

			var server = agent as Server;
			var serverCommands = new ServerCommands(agent);

			GetPort();

			keyCommands = serverCommands.KeyCommands;

			server.Start(port);
		}

		private static void SetupClient ()
		{
			Console.WriteLine("Creating client...");

			GetHost();
			GetPort();

			agent = new Client();
			var clientCommands = new ClientCommands(agent, host, port);

			keyCommands = clientCommands.KeyCommands;

			Console.WriteLine("Client ready:");

			keyCommands.Keys.ToList().ForEach(k =>
				Console.WriteLine(k + " - " + (keyCommands.TryGetValue(k, out var func)
					? func.Method.Name
					: "err")));
		}

		private static void Run ()
		{
			ConsoleKeyInfo consoleKeyInfo;

			while (isRunning)
			{
				if (Console.KeyAvailable)
				{
					consoleKeyInfo = Console.ReadKey(true);

					if (keyCommands.TryGetValue(consoleKeyInfo.KeyChar.ToString().ToLower(), out var command))
					{
						command();
					}
					else
					{
						Console.WriteLine("Unknown command");
					}
				}

				agent.Run();
			}
		}
	}
}
