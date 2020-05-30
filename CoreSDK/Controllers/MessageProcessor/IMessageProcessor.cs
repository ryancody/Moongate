using CoreNET.Controllers.Messenger;
using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public interface IMessageProcessor
	{
		void Process ();
		void Queue (ITransmittable t);
		void Receive (ConnectionId fromConnectionId, byte[] b);
		void Receive (ConnectionId fromConnectionId, IEnumerable<ITransmittable> t);
		EventHandler<MessageArgs> MessageReceived { get; set; }
	}
}
