using Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreNET.Controllers.MessageProcessor
{
	interface IMessageProcessor
	{
		void Process (ITransmittable t);
	}
}
