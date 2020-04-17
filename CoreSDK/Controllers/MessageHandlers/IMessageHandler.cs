using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	interface IMessageHandler
	{
		void Handle (Transmission m);
	}
}
