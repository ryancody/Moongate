using CoreNET.Controllers.Messenger;
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

		Queue<ITransmittable> ReceivedQueue { get; set; } = new Queue<ITransmittable>();

		public EventHandler<MessageArgs> MessageReceived { get; set; }

		public MessageProcessor (ILogger _logger, ISerializer _serializer, IHandlerFactory _handlerFactory)
		{
			logger = _logger;
			serializer = _serializer;
			handlerFactory = _handlerFactory;
		}

		public void Process ()
		{
			while (ReceivedQueue.Count > 0)
			{
				var message = ReceivedQueue.Dequeue();

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
			}
		}

		public void Queue (ITransmittable t)
		{
			ReceivedQueue.Enqueue(t);
		}

		public void Receive (ConnectionId fromConnectionId, byte[] b)
		{
			var transmittables = serializer.Deserialize<IEnumerable<ITransmittable>>(b);

			Receive(fromConnectionId, transmittables);
		}

		public void Receive (ConnectionId fromConnectionId, IEnumerable<ITransmittable> transmittables)
		{

			transmittables.ToList().ForEach(t =>
			{
				var e = new MessageArgs
				{
					Message = t
				};

				MessageReceived?.Invoke(this, e);

				t.SenderConnectionId = fromConnectionId;
				Queue(t);
			});
		}
	}
}