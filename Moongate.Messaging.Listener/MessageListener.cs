﻿using Microsoft.Extensions.Logging;
using Moongate.Models.Events;
using System;
using Telepathy;

namespace Moongate.Messaging.Listener
{
	public class MessageListener : IMessageListener
	{
		private readonly ILogger<MessageListener> logger;
		private readonly Common common;

		public event EventHandler<MessageArgs> Connected;
		public event EventHandler<MessageArgs> Disconnected;
		public event EventHandler<MessageArgs> DataReceived;

		public MessageListener (ILogger<MessageListener> logger, Common common)
		{
			this.logger = logger;
			this.common = common;
		}

		public void Listen ()
		{
			while (common.GetNextMessage(out var msg))
			{
				logger.LogDebug($@"Telepathy.Common received {msg.eventType} message");

				var args = new MessageArgs
				{
					FromConnectionId = msg.connectionId,
					Payload = msg.data
				};

				switch (msg.eventType)
				{
					case EventType.Connected:
						Connected?.Invoke(this, args);
						break;

					case EventType.Data:
						DataReceived?.Invoke(this, args);
						break;

					case EventType.Disconnected:
						Disconnected?.Invoke(this, args);
						break;

					default:
						logger.LogError($"Error listening to Telepathy message type: {msg.eventType}");
						break;
				}
			}
		}
	}
}
