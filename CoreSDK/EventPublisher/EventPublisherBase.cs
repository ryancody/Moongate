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
		public abstract EventHandler<EntityArgs> EntityUpdated { get; set; }
		public abstract EventHandler<PlayerInputArgs> PlayerInput { get; set; }

		public abstract void OnPingReceived (object sender, PingArgs args);
		public abstract void OnEntityAdded (object sender, EntityArgs args);
		public abstract void OnEntityUpdated (object sender, EntityArgs args);
		public abstract void OnPlayerInput (object sender, PlayerInputArgs args);

	}
}
