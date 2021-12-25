using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Receiver;
using Moongate.Models.Identity;
using Moongate.Models.Transmittable;
using Moq;
using Xunit;

namespace Moongate.Transmittable.Processor.Test
{
	public class TransmittableProcessorTests
	{
		private readonly TransmittableProcessor transmittableProcessor;

		private readonly Mock<ILogger<TransmittableProcessor>> mockLogger = new Mock<ILogger<TransmittableProcessor>>();
		private readonly Mock<ILogger<EntityHandler>> mockLoggerEntityHandler = new Mock<ILogger<EntityHandler>>();
		private readonly Mock<IMessageReceiver> mockMessageReceiver = new Mock<IMessageReceiver>();
		private readonly Mock<IHandlerProvider> mockHandlerProvider = new Mock<IHandlerProvider>();
		private readonly Mock<IIdentityProvider> mockIdentityProvider = new Mock<IIdentityProvider>();

		public TransmittableProcessorTests ()
		{
			transmittableProcessor = new TransmittableProcessor(mockLogger.Object,
											mockMessageReceiver.Object,
											mockHandlerProvider.Object,
											mockIdentityProvider.Object);
		}

		public Transmission GetTransmission (string guid) =>
			 new Transmission
			 {
				 SenderConnectionId = 0,
				 SenderGuid = guid,
				 TransmissionType = TransmissionType.NetEvent
			 };

		[Fact]
		public void Process_DoesntProcessMessagesThatAreMyOwn_Successfully ()
		{
			var transmission = GetTransmission("My Guid");

			mockIdentityProvider.Setup(i => i.Id)
				.Returns(new Id
				{
					Guid = "My Guid"
				})
				.Verifiable();

			mockHandlerProvider.Setup(h => h.GetHandler(TransmissionType.EntityTransmit))
				.Returns(new EntityHandler(mockLoggerEntityHandler.Object))
				.Verifiable();

			transmittableProcessor.Process(transmission);

			mockHandlerProvider.Verify(h => h.GetHandler(TransmissionType.NetEvent), Times.Never);
			mockIdentityProvider.Verify();
		}

		[Fact]
		public void Process_ProcessesMessagesThatArentMyOwn_Successfully ()
		{
			var transmission = GetTransmission("Some Other Guid");

			mockIdentityProvider.Setup(i => i.Id)
				.Returns(new Id
				{
					Guid = "Someone Else's Guid"
				})
				.Verifiable();

			transmittableProcessor.Process(transmission);

			mockHandlerProvider.Verify(h => h.GetHandler(TransmissionType.NetEvent), Times.Once);
			mockIdentityProvider.Verify();
		}

		// TODO: Figure out how to mock event
		//[Fact]
		//public void OnTransmissionReceived_Triggered_Success ()
		//{
		//	var args = new TransmissionArgs
		//	{
		//		Transmission = new Transmission
		//		{
		//			TransmissionType = TransmissionType.EntityTransmit
		//		}
		//	};

		//	SetupMessageReceiverEvent(args);

		//	mockMessageReceiver.Raise(m => m.TransmissionReceived += null, args);

		//	mockHandlerProvider.Setup(h => h.GetHandler(TransmissionType.EntityTransmit))
		//		.Verifiable();

		//	mockHandlerProvider.Verify();
		//}

		//private void SetupMessageReceiverEvent (TransmissionArgs args)
		//{
		//	mockMessageReceiver.Setup(m => m.TriggerTransmissionReceived(args))
		//						.Raises(i => i.TransmissionReceived += transmittableProcessor.OnTransmissionReceived, null, args);
		//}
	}
}
