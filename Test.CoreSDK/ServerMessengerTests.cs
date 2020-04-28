using CoreSDK;
using CoreSDK.Controllers;
using CoreSDK.Factory;
using CoreSDK.Models;
using CoreSDK.Utils;
using Moq;
using System;
using System.Collections.Generic;
using Telepathy;
using Xunit;
using Xunit.Sdk;

namespace Test.CoreSDK
{
	public class ServerMessengerTests
	{
		private Mock<IServerMessenger> serverMessenger;
		private Mock<ILogger> logger;
		private Mock<Server> server;
		private Mock<PlayerStateController> playerStateController;
		private Mock<ITransmittableFactory> transmittableFactory;
		private Mock<IHandlerFactory> handlerFactory;
		private Mock<ISerializer> serializer;
		
		public ServerMessengerTests ()
		{
			logger = new Mock<ILogger>();
			server = new Mock<Server>();
			playerStateController = new Mock<PlayerStateController>();
			transmittableFactory = new Mock<ITransmittableFactory>();
			handlerFactory = new Mock<IHandlerFactory>();
			serializer = new Mock<ISerializer>();

			serverMessenger = new Mock<IServerMessenger>();
			serverMessenger.SetupAllProperties();
//			serverMessenger = new ServerMessenger(logger.Object, server.Object, playerState.Object, transmittableFactory.Object, handlerFactory.Object, serializer.Object);
		}

		[Fact]
		public void BroadcastTest ()
		{
			var t = new Transmission(MessageType.Ping, null);
			transmittableFactory.Setup(x => x.Build(MessageType.Ping, null)).Returns(t);
			
			//var tf = transmittableFactory.Object.Build(MessageType.Ping, null);

			playerStateController.Object.AddPlayer(0, "a", "nameA");
			playerStateController.Object.AddPlayer(1, "b", "nameB");
			playerStateController.Object.AddPlayer(2, "c", "nameC");

			serverMessenger.Object.Broadcast(t);


		}
	}
}
