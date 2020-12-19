using Moongate.Logger;
using Moongate.Models.Transmittable;
using Moq;
using System;
using Xunit;

namespace Moongate.Messaging.Handler.Tests
{
	public class MessagingHandlerTests
	{
		private readonly IHandlerProvider handlerProvider;
		private readonly Mock<ILogger> mockLogger = new Mock<ILogger>();

		public MessagingHandlerTests ()
		{
			handlerProvider = new HandlerProvider(mockLogger.Object);
		}

		[Theory]
		[InlineData(TransmissionType.PlayerConnected, typeof(PlayerConnectedHandler))]
		[InlineData(TransmissionType.PlayerHandshake, typeof(PlayerHandshakeHandler))]
		[InlineData(TransmissionType.PlayerDisconnected, typeof(PlayerDisconnectedHandler))]
		[InlineData(TransmissionType.Ping, typeof(PingHandler))]
		public void GetHandler_IsPassedHandlerType_ReturnsProperHandler (TransmissionType transmissionType, Type transmissionHandler)
		{
			var handler = handlerProvider.GetHandler(transmissionType);

			Assert.NotNull(handler);
			Assert.IsType(transmissionHandler, handler);
		}

		[Fact]
		public void GetHandler_IsPassedBogusType_ThrowsException ()
		{
			Assert.Throws<Exception>(() => handlerProvider.GetHandler(TransmissionType.COUNT));
		}
	}
}
