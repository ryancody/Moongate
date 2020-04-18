using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class EntityHandler : IMessageHandler
	{
		public static event EventHandler<EntityArgs> EntityReceived;

		readonly ILogger logger;

		public EntityHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (Transmission message)
		{
			var entity = (Entity)message.Payload;

			var args = new EntityArgs()
			{
				Entity = entity
			};

			logger.Debug($@"InstantiateEntityHandler
			 - {args.Entity}");

			EntityReceived?.Invoke(this, args);
		}
	}
}
