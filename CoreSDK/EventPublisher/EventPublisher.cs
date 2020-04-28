using CoreSDK.Factory;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public class EventPublisher : EventPublisherBase
	{
		readonly ILogger logger;
		readonly IHandlerFactory handlerFactory;

		public override EventHandler<PingArgs> PingReceived { get; set; }
		public override EventHandler<EntityArgs> EntityAdded { get; set; }

		public EventPublisher (ILogger _logger, IHandlerFactory _handlerFactory) 
		{
			logger = _logger;
			handlerFactory = _handlerFactory;

			((PingHandler)handlerFactory.GetHandler(MessageType.Ping)).PingReceived += OnPingReceived;
			((EntityHandler)handlerFactory.GetHandler(MessageType.EntityUpdate)).EntityReceived += OnEntityReceived;
		}

		public override void OnPingReceived (object sender, PingArgs args)
		{
			PingReceived.Invoke(this, args);
		}

		public override void OnEntityReceived (object sender, EntityArgs args)
		{
			EntityAdded.Invoke(this, args);
		}
	}
}
