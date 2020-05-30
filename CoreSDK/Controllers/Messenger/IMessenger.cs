using CoreSDK.Models;
using System.Collections.Generic;

namespace CoreSDK.Controllers
{
	public interface IMessenger
	{
		void QueueTransmission (ITransmittable message);
		void TransmitQueue ();
	}
}
