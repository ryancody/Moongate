using CoreSDK.Controllers;
using CoreSDK.Models;
using System;
using System.Xml;

namespace CoreSDK
{
	public class EntityHandler : IMessageHandler
	{
		public event EventHandler<EntityArgs> EntityReceived;

		readonly ILogger logger;

		public EntityHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (ITransmittable message)
		{
			var args = (EntityArgs)message.Payload;

			logger.Debug($@"InstantiateEntityHandler
			 - {args.Entity}");

			EntityReceived?.Invoke(this, args);
		}
	}
}
