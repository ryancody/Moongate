﻿using Moongate.Network;
using Moongate.Network.Events;
using Network.Models;
using System;

namespace Network
{
	class Program
	{
		static readonly Client client = new Client();

		static void Main (string[] args)
		{
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
								Owner = LocalId.Guid,
								Position = new Position() { x = 0, y = 0 }
							};
							var entityArgs = new EntityArgs()
							{
								Entity = entity
							};
							var entityTransmission = new Transmission()
							{
								MessageType = MessageType.EntityTransmit,
								Payload = entityArgs,
								SenderGuid = LocalId.Guid
							};
							client.QueueTransmission(entityTransmission);
							break;

						case 'p':
							Console.WriteLine("ping server");
							client.Ping();
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
