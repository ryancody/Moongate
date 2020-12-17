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

		private void OnDataReceived (object sender, MessageArgs messageArgs)
		{
			Receive(messageArgs.FromConnectionId, messageArgs.Payload);
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
			IEnumerable<ITransmittable> transmittables = null;

			try
			{

				transmittables = serializer.Deserialize<IEnumerable<ITransmittable>>(byteArray);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}


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