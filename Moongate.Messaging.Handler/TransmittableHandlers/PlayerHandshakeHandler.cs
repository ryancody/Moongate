using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerHandshakeHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<ClientArgs> PlayerHandshake;

		public PlayerHandshakeHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var clientArgs = (ClientArgs)t.Payload;
			clientArgs.ConnectionId = t.SenderConnectionId.GetValueOrDefault();

			logger.Debug($@"PlayerHandshakeHandler: 
			 - {clientArgs.Name}
			 - {clientArgs.Guid}
			 - {clientArgs.ConnectionId}");

			PlayerHandshake?.Invoke(this, clientArgs);
		}
	}
}