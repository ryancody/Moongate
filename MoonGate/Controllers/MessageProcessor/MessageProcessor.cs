using Moongate.Network.Events;
using Network;
using Network.Factory;
using Network.Models;
using System;

namespace CoreNET.Controllers.MessageProcessor
{
	class MessageProcessor : IMessageProcessor
	{
		private readonly ILogger logger;
		private readonly IHandlerFactory handlerFactory;
		private readonly IMessageReceiver messageReceiver;

		public MessageProcessor (ILogger logger, IMessageReceiver messageReceiver, IHandlerFactory handlerFactory)
		{
			this.logger = logger;
			this.handlerFactory = handlerFactory;
			this.messageReceiver = messageReceiver;

			this.messageReceiver.MessageReceived += OnMessageReceived;
		}

		public void OnMessageReceived (object sender, MessageArgs a) 
		{
			Process(a.Message);
		}

		public void Process (ITransmittable message)
		{
			try
			{
				if (message.SenderGuid != LocalId.Guid)
				{
					var handler = handlerFactory.GetHandler(message.MessageType);

					handler.Handle(message);

					logger.Debug($@"Message handled:
						- {message.SenderGuid}
						- {message.MessageType}");
				}
			}
			catch (Exception e)
			{
				logger.Error(e.ToString());
				Console.WriteLine(e);
			}
		}
	}
}
