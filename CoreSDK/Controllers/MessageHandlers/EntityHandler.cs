using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class EntityHandler : IMessageHandler
	{
		public static event EventHandler<BasicPlayerRequestArgs> EntityReceived;

		readonly ILogger logger;

		public EntityHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (Transmission message)
		{
			var entity = (Entity)message.Payload;

			var args = new BasicPlayerRequestArgs()
			{
				ClientGuid = message.SenderGuid,
				Payload = entity
			};

			logger.Debug($@"InstantiateEntityHandler
			 - {args.ConnectionId}
			 - {args.ClientGuid} 
			 - {args.PlayerName}");

			EntityReceived?.Invoke(this, args);
		}
	}
}
