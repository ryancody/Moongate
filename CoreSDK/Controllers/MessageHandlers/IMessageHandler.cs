using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	public interface IMessageHandler
	{
		void Handle (ITransmittable m);
	}
}
