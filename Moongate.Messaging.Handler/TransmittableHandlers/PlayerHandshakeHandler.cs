using Microsoft.Extensions.Logging;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class PlayerHandshakeHandler : BaseHandler, ITransmissionHandler
	{
		private readonly ILogger<PlayerHandshakeHandler> logger;

		public event EventHandler<ClientArgs> PlayerHandshake;

		public PlayerHandshakeHandler (ILogger<PlayerHandshakeHandler> logger) : base() 
		{
			this.logger = logger;
		}

		public void Handle (ITransmittable t)
		{
			var clientArgs = (ClientArgs)t.Payload;
			clientArgs.ConnectionId = t.SenderConnectionId.GetValueOrDefault();

			logger.LogDebug($@"PlayerHandshakeHandler: 
			 - {clientArgs.Name}
			 - {clientArgs.Guid}
			 - {clientArgs.ConnectionId}");

			PlayerHandshake?.Invoke(this, clientArgs);
		}
	}
}