using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Utils;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Moongate.Messaging.Receiver.Test
{
	public class MessageReceiverTests
	{
		private readonly MessageReceiver messageReceiver;

		private readonly Mock<ILogger> mockLogger = new Mock<ILogger>();
		private readonly Mock<ISerializer> mockSerializer = new Mock<ISerializer>();
		private readonly Mock<IMessageListener> mockMessageListener = new Mock<IMessageListener>();

		public MessageReceiverTests ()
		{
			messageReceiver = new MessageReceiver(mockLogger.Object, mockSerializer.Object, mockMessageListener.Object);
		}

		[Fact]
		public void Receive_InsertsFromId_Successfully ()
		{
			var receivedEvents = new List<ITransmittable>();

			mockSerializer.Setup(s => s.Deserialize<IEnumerable<ITransmittable>>(It.IsAny<byte[]>()))
			.Returns(new List<ITransmittable> 
			{
				new Transmission
				{ 
					 SenderConnectionId = 1
				},
				new Transmission
				{
					 SenderConnectionId = 2
				}
			});

			messageReceiver.TransmissionReceived += delegate (object sender, TransmissionArgs transmissionArgs)
			{
				receivedEvents.Add(transmissionArgs.Transmission);
			};

			messageReceiver.Receive(0, new byte[] { });

			Assert.Equal(0, receivedEvents[0].SenderConnectionId);
			Assert.Equal(0, receivedEvents[1].SenderConnectionId);
		}
	}
}
