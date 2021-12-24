using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerDisconnectedHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<ClientArgs> PlayerDisconnected;

		public PlayerDisconnectedHandler () : base() { }

		public void Handle (ITransmittable t)
		{
			var clientArgs = new ClientArgs
			{
				ConnectionId = t.SenderConnectionId.GetValueOrDefault()
			};

			PlayerDisconnected?.Invoke(this, clientArgs);
		}
	}
}
