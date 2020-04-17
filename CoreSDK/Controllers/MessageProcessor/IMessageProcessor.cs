using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public interface IMessageProcessor
	{
		void Process ();

		void Receive (int fromConnectionId, byte[] b);

		void Receive (byte[] b);
	}
}
