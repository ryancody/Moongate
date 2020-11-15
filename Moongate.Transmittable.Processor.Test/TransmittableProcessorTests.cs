using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Receiver;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moq;
using Xunit;

namespace Moongate.Transmittable.Processor.Test
{
	public class TransmittableProcessorTests
	{
		private readonly TransmittableProcessor transmittableProcessor;

		private readonly Mock<ILogger> mockLogger = new Mock<ILogger>();
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

		[Fact]
		public void OnTransmissionReceived_Triggered_Success ()
		{
			var args = new TransmissionArgs
			{
				Transmission = new Transmission
				{
					TransmissionType = TransmissionType.EntityTransmit
				}
			};

			SetupMessageReceiverEvent(args);

			mockMessageReceiver.Raise(m => m.TransmissionReceived += null, args);

			mockHandlerProvider.Setup(h => h.GetHandler(TransmissionType.EntityTransmit))
				.Verifiable();

			mockHandlerProvider.Verify();
		}

		private void SetupMessageReceiverEvent (TransmissionArgs args)
		{
			mockMessageReceiver.Setup(m => m.TriggerTransmissionReceived(args))
								.Raises(i => i.TransmissionReceived += transmittableProcessor.OnTransmissionReceived, null, args);
		}
	}
}
