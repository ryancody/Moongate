using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Receiver;
using Moongate.Models;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.TransmittableProcessor
{
	class TransmittableProcessor : ITransmittableProcessor
	{
		private readonly ILogger logger;
		private readonly IHandlerProvider handlerFactory;
		private readonly IMessageReceiver messageReceiver;

		public TransmittableProcessor (ILogger logger, IMessageReceiver messageReceiver, IHandlerProvider handlerFactory)
		{
			this.logger = logger;
			this.handlerFactory = handlerFactory;
			this.messageReceiver = messageReceiver;

			this.messageReceiver.TransmissionReceived += OnTransmissionReceived;
		}

		public void OnTransmissionReceived (object sender, TransmissionArgs a) 
		{
			Process(a.Transmission);
		}

		public void Process (ITransmittable message)
		{
			try
			{
				if (message.SenderGuid != LocalId.Guid)
				{
					var handler = handlerFactory.GetHandler(message.TransmissionType);

					handler.Handle(message);

					logger.Debug($@"Message handled:
						- {message.SenderGuid}
						- {message.TransmissionType}");
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
