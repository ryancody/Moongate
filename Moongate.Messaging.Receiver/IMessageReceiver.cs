using Moongate.Models.Events;
using System;

namespace Moongate.Messaging.Receiver
{
	public interface IMessageReceiver
	{
		EventHandler<TransmissionArgs> TransmissionReceived { get; set; }
		void TriggerTransmissionReceived (TransmissionArgs args);
	}
}
