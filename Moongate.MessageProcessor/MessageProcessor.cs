using Moongate.HandlerFactory;
using Moongate.Logger;
using Moongate.MessageReceiver;
using Moongate.Models;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.MessageProcessor
{
	class MessageProcessor : IMessageProcessor
	{
		private readonly ILogger logger;
		private readonly IHandlerFactory handlerFactory;
		private readonly IMessageReceiver messageReceiver;

		private readonly EventArgs eventArgs;

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
