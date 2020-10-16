using Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Controllers
{
	public interface IMessageHandler
	{
		void Handle (ITransmittable m);
	}
}
