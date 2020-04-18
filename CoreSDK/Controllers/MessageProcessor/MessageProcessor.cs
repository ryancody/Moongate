using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSDK
{
	public class MessageProcessor : IMessageProcessor
	{
		private readonly ILogger logger;
		private readonly HandlerFactory handlerFactory;

		List<Transmission> ReceivedQueue { get; set; }


		public MessageProcessor (ILogger l)
		{
			logger = l;
			ReceivedQueue = new List<Transmission>();
			handlerFactory = new HandlerFactory(logger);
		}

		public void Process ()
		{
			while (ReceivedQueue.Count > 0)
			{
				Transmission message = ReceivedQueue.First();

				try
				{
					var handler = handlerFactory.GetHandler(message.MessageType);

					handler.Handle(message);

					logger.Debug($@"Message handled:
					 - {message.SenderConnectionId}
					 - {message.SenderGuid}
					 - {message.MessageType}");
				}
				catch (Exception e)
				{
					logger.Error(e.ToString());
					Console.WriteLine(e);
				}

				ReceivedQueue.Remove(message);
			}
		}

		public void Receive (byte[] b)
		{
			Transmission t = Transmittable.Deserialized(b);

			ReceivedQueue.Add(t);
		}

		public void Receive (int fromConnectionId, byte[] b)
		{
			Transmission t = Transmittable.Deserialized(b);

			t.SenderConnectionId = fromConnectionId;
			ReceivedQueue.Add(t);
		}
	}
}