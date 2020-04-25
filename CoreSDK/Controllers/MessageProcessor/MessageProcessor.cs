using CoreSDK.Factory;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSDK
{
	public class MessageProcessor : IMessageProcessor
	{
		private readonly ILogger logger;
		private readonly ISerializer serializer;
		private readonly IHandlerFactory handlerFactory;

		List<ITransmittable> ReceivedQueue { get; set; }

		public MessageProcessor (ILogger _logger, ISerializer _serializer, IHandlerFactory _handlerFactory)
		{
			logger = _logger;
			serializer = _serializer;
			ReceivedQueue = new List<ITransmittable>();
			handlerFactory = _handlerFactory;
		}

		public void Process ()
		{
			while (ReceivedQueue.Count > 0)
			{
				ITransmittable message = ReceivedQueue.First();

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
			var t = serializer.Deserialize<ITransmittable>(b);

			Receive(t);
		}

		public void Receive (ITransmittable t)
		{
			ReceivedQueue.Add(t);
		}

		public void Receive (int fromConnectionId, byte[] b)
		{
			var t = serializer.Deserialize<ITransmittable>(b);

			t.SenderConnectionId = fromConnectionId;
			Receive(t);
		}
	}
}