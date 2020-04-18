using CoreSDK;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	class Program
	{
		static readonly CoreClient client = new CoreClient();

		static void Main (string[] args)
		{
			var running = true;
			ConsoleKeyInfo consoleKeyInfo;

			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Console.WriteLine(version);

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

						case 't':
							Console.WriteLine("Sending test message");
							client.Transmit(new Transmission(MessageType.Default, null));
							break;

						case 'p':
							Console.WriteLine("ping server");
							client.Ping();
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
