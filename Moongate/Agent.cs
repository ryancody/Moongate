﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moongate.Events.Reactor;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Listener;
using Moongate.Messaging.Messenger;
using Moongate.Messaging.Receiver;
using Moongate.State.Controller;
using Moongate.State.Models;
using Moongate.Transmittable.Processor;
using System;
using System.Collections.Generic;

namespace Moongate
{
	public class Agent
	{
		public List<Player> ConnectedPlayers { get { return playerStateController.GetPlayers(); } }

		protected readonly DependencyInjection dependencyInjection;
		protected readonly ILogger<Agent> logger;
		protected readonly IMessageListener messageListener;
		protected readonly IMessageReceiver messageReceiver;
		protected readonly ITransmittableProcessor transmittableProcessor;
		protected readonly IHandlerProvider handlerProvider;
		protected readonly EventReactor eventReactor;
		protected readonly IMessenger messenger;
		protected readonly IIdentityProvider identityProvider;
		protected readonly PlayerStateController playerStateController;

		internal Agent (bool isServer)
		{
			dependencyInjection = new DependencyInjection(isServer);

<<<<<<< HEAD
			logger = dependencyInjection.ServiceProvider.GetRequiredService<ILogger<Agent>>();
			messageListener = dependencyInjection.ServiceProvider.GetRequiredService<IMessageListener>();
			messageReceiver = dependencyInjection.ServiceProvider.GetRequiredService<IMessageReceiver>();
			transmittableProcessor = dependencyInjection.ServiceProvider.GetRequiredService<ITransmittableProcessor>();
			handlerProvider = dependencyInjection.ServiceProvider.GetRequiredService<IHandlerProvider>();
			eventReactor = dependencyInjection.ServiceProvider.GetRequiredService<EventReactor>();
			messenger = dependencyInjection.ServiceProvider.GetRequiredService<IMessenger>();
			identityProvider = dependencyInjection.ServiceProvider.GetRequiredService<IIdentityProvider>();
=======
			logger = dependencyInjection.Services.GetService<ILogger>();
			messageListener = dependencyInjection.Services.GetService<IMessageListener>();
			messageReceiver = dependencyInjection.Services.GetService<IMessageReceiver>();
			transmittableProcessor = dependencyInjection.Services.GetService<ITransmittableProcessor>();
			handlerProvider = dependencyInjection.Services.GetService<IHandlerProvider>();
			eventReactor = dependencyInjection.Services.GetService<EventReactor>();
			messenger = dependencyInjection.Services.GetService<IMessenger>();
			identityProvider = dependencyInjection.Services.GetService<IIdentityProvider>();
			playerStateController = dependencyInjection.Services.GetService<PlayerStateController>();

			var role = isServer ? "Server" : "Client";
			logger.Info($@"{role}
			 - Time: {DateTime.Now}
			 - Instance Name: {identityProvider.Id?.Name}
			 - GUID: {identityProvider.Id?.Guid}");
>>>>>>> protobuf
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
				logger.LogError(e.ToString());
				Console.WriteLine(e);
			}
		}
	}
}
