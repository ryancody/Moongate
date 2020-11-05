using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moongate.Messaging.Receiver
{
	public class MessageReceiver : IMessageReceiver
	{
		private readonly ILogger logger;
		private readonly ISerializer serializer;

		public EventHandler<TransmissionArgs> TransmissionReceived { get; set; }
		
		public MessageReceiver (ILogger logger, ISerializer serializer, IMessageListener messageListener)
		{
			this.logger = logger;
			this.serializer = serializer;

			messageListener.DataReceived += OnDataReceived;
		}

		public void OnDataReceived (object sender, MessageArgs messageArgs)
		{
			Receive(messageArgs.SenderConnectionId, messageArgs.Payload);
		}

		/// <summary>
		/// Queues a single transmittable.
		/// </summary>
		/// <param name="transmittable"></param>
		void Receive (int? fromConnectionId, ITransmittable transmittable)
		{
			transmittable.SenderConnectionId = fromConnectionId;

			var e = new TransmissionArgs() 
			{ 
				Transmission = transmittable				
			};
			TransmissionReceived?.Invoke(this, e);
		}

		/// <summary>
		/// Deserializes a batch of transmittables and sends them to be queued up by Receive.
		/// </summary>
		/// <param name="byteArray"></param>
		void Receive (int? fromConnectionId, byte[] byteArray)
		{
			var transmittables = serializer.Deserialize<IEnumerable<ITransmittable>>(byteArray);

			Receive(fromConnectionId, transmittables);
		}

		/// <summary>
		/// Receives a batch of transmittables and queues them up.  Fires MessageReceived after they are queued.
		/// </summary>
		/// <param name="transmittables"></param>
		void Receive (int? fromConnectionId, IEnumerable<ITransmittable> transmittables)
		{
			transmittables.Select(t =>
			{
				Receive(fromConnectionId, t);
			});
		}
	}
}