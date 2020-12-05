using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Models;
using System;

namespace Moongate
{
	class Program
	{
		private static Client client;

		static void Main (string[] args)
		{
			client = new Client();

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

					switch (consoleKeyInfo.KeyChar)
					{
						case 'c':
							Console.WriteLine("Connecting...");
							client.Connect("localhost", 8888);
							break;

						case 'd':
							Console.WriteLine("Disconnecting...");
							client.Disconnect();
							break;

						case 'e':
							Console.WriteLine("creating entity");
							var entity = new Entity()
							{
								Guid = Guid.NewGuid().ToString(),
								Id = "test ID",
								Name = "test Name",
								Owner = client.Id.Guid
							};

							client.Farspeaker.Input.TransmitEntity(entity);
							break;

						case 'p':
							Console.WriteLine("ping server");
							client.Farspeaker.Input.Ping();
							break;

						default:
							Console.WriteLine("unknown command");
							break;
					}

					if (consoleKeyInfo.Key == ConsoleKey.Escape)
					{
						consoleKeyInfo = Console.ReadKey(true);

						Console.WriteLine("press 'q' to quit");

						if (consoleKeyInfo.KeyChar == 'q')
						{
							running = false;
							client.Disconnect();
						}
					}
				}

				client.Run();
			}

			Console.WriteLine("Hello World!");
		}
	}
}
