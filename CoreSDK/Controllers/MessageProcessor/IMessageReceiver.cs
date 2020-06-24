﻿using CoreSDK.Models;
using System;
using System.Collections.Generic;

namespace CoreSDK
{
	public interface IMessageReceiver
	{
		void Receive (int? fromConnectionId, ITransmittable t);
		void Receive (int? fromConnectionId, byte[] b);
		void Receive (int? fromConnectionId, IEnumerable<ITransmittable> t);
		EventHandler<MessageArgs> MessageReceived { get; set; }
	}
}
