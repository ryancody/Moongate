﻿using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Moongate.Messaging.Receiver.Test")]
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

		internal void TriggerTransmissionReceived (TransmissionArgs args)
		{
			TransmissionReceived?.Invoke(this, args);
		}

		/// <summary>
		/// Deserializes a batch of transmittables and sends them to TransmittableProcessor.
		/// </summary>
		/// <param name="byteArray"></param>
		internal void Receive (int? fromConnectionId, byte[] byteArray)
		{
			var transmittables = serializer.Deserialize<IEnumerable<ITransmittable>>(byteArray);

			transmittables.ToList().ForEach(t =>
			{
				var transmissionArgs = new TransmissionArgs
				{
					Transmission = new Transmission
					{
						Payload = t.Payload,
						SenderConnectionId = fromConnectionId,
						SenderGuid = t.SenderGuid,
						TransmissionType = t.TransmissionType
					}
				};

				TriggerTransmissionReceived(transmissionArgs);
			});
		}
	}
}