using Moongate.Logger;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.State.Models;
using Moongate.Utils;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Moongate.Messaging.Messenger.Tests
{
	public class ServerMessengerTests
	{
		private readonly ServerMessenger serverMessenger;

		private readonly Mock<ILogger> mockLogger = new Mock<ILogger>();
		private readonly Mock<ISerializer> mockSerializer = new Mock<ISerializer>();
		private readonly Mock<Telepathy.Server> mockTelepathyServer = new Mock<Telepathy.Server>();
		private readonly Mock<PlayerStateController> mockPlayerStateController;

		public ServerMessengerTests ()
		{
			mockPlayerStateController = new Mock<PlayerStateController>(mockLogger.Object, new PlayerState());

			serverMessenger = new ServerMessenger(mockLogger.Object, mockSerializer.Object, mockTelepathyServer.Object, mockPlayerStateController.Object);
		}

		[Fact]
		public void BuildQueues ()
		{
			var queue = new List<Transmission>
			{
				new Transmission
				{
					 SenderGuid = "abc"
				},
				new Transmission
				{
					ToConnectionId = 1,
					 SenderGuid = "def"
				},
				new Transmission
				{
					ToConnectionId = 2,
					 SenderGuid = "ghi"
				}
			};

			var actual = serverMessenger.BuildQueues(queue);

			var sizes = actual.Select(a => a.Count());

			Assert.True(sizes.All(i => i == 1));
		}
	}
}
