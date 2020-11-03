using Network.Models;
using System.Collections.Generic;

namespace Moongate.Messenger
{
	public interface IMessenger
	{
		void QueueTransmission (ITransmittable message);
		void TransmitQueue ();
	}
}
