using Network.Models;
using System.Collections.Generic;

namespace Network.Controllers
{
	public interface IMessenger
	{
		void QueueTransmission (ITransmittable message);
		void TransmitQueue ();
	}
}
