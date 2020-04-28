using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public abstract class EventPublisherBase
	{
		public abstract EventHandler<PingArgs> PingReceived { get; set; }
		public abstract EventHandler<EntityArgs> EntityAdded { get; set; }

		public abstract void OnPingReceived (object sender, PingArgs args);
		public abstract void OnEntityReceived (object sender, EntityArgs args);

	}
}
