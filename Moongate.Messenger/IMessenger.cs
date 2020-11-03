using Moongate.Transmittable.Models;

namespace Moongate.Messenger
{
	public interface IMessenger
	{
		void QueueTransmission (ITransmittable message);
		void TransmitQueue ();
	}
}
