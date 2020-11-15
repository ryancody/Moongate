using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class EntityHandler : BaseHandler, ITransmissionHandler
	{
		public event EventHandler<EntityArgs> EntityReceived;

		public EntityHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable t)
		{
			var args = (EntityArgs)t.Payload;

			logger.Debug($@"InstantiateEntityHandler
			 - {args.Entity}");

			EntityReceived?.Invoke(this, args);
		}
	}
}
