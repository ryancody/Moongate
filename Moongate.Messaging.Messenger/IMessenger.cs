using Moongate.Models.Transmittable;

namespace Moongate.Messaging.Messenger
{
	public interface IMessenger
	{
		void QueueTransmission (ITransmittable message);
		void TransmitQueue ();
	}
}
