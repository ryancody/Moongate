﻿using Network;
using Network.Controllers;
using Network.Factory;
using Network.Utils;
using Moq;
using Telepathy;

namespace Test.CoreSDK
{
	public class ServerMessengerTests
	{
		private Mock<IMessenger> serverMessenger;
		private Mock<ILogger> logger;
		private Mock<Telepathy.Server> server;
		private Mock<PlayerStateController> playerStateController;
		private Mock<ITransmittableFactory> transmittableFactory;
		private Mock<IHandlerFactory> handlerFactory;
		private Mock<ISerializer> serializer;
		
		public ServerMessengerTests ()
		{
			logger = new Mock<ILogger>();
			server = new Mock<Telepathy.Server>();
			playerStateController = new Mock<PlayerStateController>();
			transmittableFactory = new Mock<ITransmittableFactory>();
			handlerFactory = new Mock<IHandlerFactory>();
			serializer = new Mock<ISerializer>();

			serverMessenger = new Mock<IMessenger>();
			serverMessenger.SetupAllProperties();
//			serverMessenger = new ServerMessenger(logger.Object, server.Object, playerState.Object, transmittableFactory.Object, handlerFactory.Object, serializer.Object);
		}

	}
}