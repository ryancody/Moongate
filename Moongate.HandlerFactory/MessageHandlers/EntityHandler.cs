using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.Messaging.Handler
{
	public class EntityHandler : BaseHandler, IMessageHandler
	{
		public event EventHandler<EntityArgs> EntityReceived;

		public EntityHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable message)
		{
			var args = (EntityArgs)message.Payload;

			logger.Debug($@"InstantiateEntityHandler
			 - {args.Entity}");

			EntityReceived?.Invoke(this, args);
		}
	}
}
