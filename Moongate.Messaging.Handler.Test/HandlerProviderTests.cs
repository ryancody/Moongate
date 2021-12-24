using Moongate.Models.Transmittable;
using System;
using Xunit;

namespace Moongate.Messaging.Handler.Tests
{
	public class MessagingHandlerTests
	{
		private readonly IHandlerProvider handlerProvider;

		public MessagingHandlerTests ()
		{

		}

		[Theory]
		[InlineData(TransmissionType.PlayerConnected, typeof(PlayerConnectedHandler))]
		[InlineData(TransmissionType.PlayerHandshake, typeof(PlayerHandshakeHandler))]
		[InlineData(TransmissionType.PlayerDisconnected, typeof(PlayerDisconnectedHandler))]
		[InlineData(TransmissionType.PlayerInput, typeof(PlayerInputHandler))]
		[InlineData(TransmissionType.GameStateRequest, typeof(GameStateRequestHandler))]
		[InlineData(TransmissionType.EntityTransmit, typeof(EntityHandler))]
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
