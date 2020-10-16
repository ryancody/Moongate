using Network.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Network
{
	public abstract class EventPublisherBase
	{
		public abstract EventHandler<PingArgs> PingReceived { get; set; }
		public abstract EventHandler<EntityArgs> EntityAdded { get; set; }
		public abstract EventHandler<EntityArgs> EntityUpdated { get; set; }
		public abstract EventHandler<ControlArgs> PlayerInputChanged { get; set; }

		public abstract void OnPingReceived (object sender, PingArgs args);
		public abstract void OnEntityAdded (object sender, EntityArgs args);
		public abstract void OnEntityUpdated (object sender, EntityArgs args);
		public abstract void OnPlayerInputChanged (object sender, ControlArgs args);

	}
}
