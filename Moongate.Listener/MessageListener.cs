using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;
using Telepathy;

namespace Moongate.MessageListener
{
	class MessageListener
	{
		private readonly ILogger logger;
		private readonly Common common;

		public static event EventHandler<MessageArgs> Connected;
		public static event EventHandler<MessageArgs> Disconnected;
		public static event EventHandler<MessageArgs> DataReceived;

		public MessageListener (ILogger logger, Common common)
		{
			this.logger = logger;
			this.common = common;
		}

		public void Listen ()
		{
			while (common.GetNextMessage(out var msg))
			{
				logger.Debug($@"Telepathy.Common received {msg.eventType} message");

				switch (msg.eventType)
				{
					case EventType.Connected:
						var connectedArgs = new MessageArgs
						{
							Message = new Transmission
							{
								SenderConnectionId = msg.connectionId
							}
						};

						DataReceived?.Invoke(this, connectedArgs);
						break;

					case EventType.Data:
						var dataArgs = new MessageArgs
						{
							Message = new Transmission
							{
								SenderConnectionId = msg.connectionId,
								Payload = msg.data
							}
						};

						DataReceived?.Invoke(this, dataArgs);
						break;

					case EventType.Disconnected:
						var disconnectedArgs = new MessageArgs
						{
							Message = new Transmission
							{
								SenderConnectionId = msg.connectionId
							}
						};

						DataReceived?.Invoke(this, disconnectedArgs);
						break;

					default:
						logger.Error($"Error listening to Telepathy message type: {msg.eventType}");
						break;
				}
			}
		}
	}
}
