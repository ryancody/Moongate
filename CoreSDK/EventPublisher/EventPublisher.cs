using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.EventPublisher
{
	public class EventPublisher : IEventPublisher
	{
		public EventHandler d { private get; set; }

		public EventPublisher () 
		{
			
		}
	}
}
