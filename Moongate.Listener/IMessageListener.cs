using Moongate.Models.Events;
using System;

namespace Moongate.Messaging.Listener
{
	public interface IMessageListener
	{
		void Listen ();

		event EventHandler<MessageArgs> Connected;
		event EventHandler<MessageArgs> Disconnected;
		event EventHandler<MessageArgs> DataReceived;
	}
}
