using Microsoft.Extensions.Logging;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Messaging.Handler
{
	public class EntityHandler : BaseHandler, ITransmissionHandler
	{
		private readonly ILogger<EntityHandler> logger;

		public event EventHandler<EntityArgs> EntityReceived;

		public EntityHandler (ILogger<EntityHandler> logger) : base() 
		{
			this.logger = logger;
		}

		public void Handle (ITransmittable t)
		{
			var args = (EntityArgs)t.Payload;

			logger.LogDebug($@"InstantiateEntityHandler
			 - {args.Entity}");

			EntityReceived?.Invoke(this, args);
		}
	}
}
