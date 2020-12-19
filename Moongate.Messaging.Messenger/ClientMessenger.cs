using Moongate.Logger;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using TelepathyClient = Telepathy.Client;

namespace Moongate.Messaging.Messenger
{
	public class ClientMessenger : IMessenger
	{
		private readonly ILogger logger;
		private readonly ISerializer serializer;
		private readonly TelepathyClient telepathyClient;

		private Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ClientMessenger (ILogger logger,
			ISerializer serializer,
			TelepathyClient telepathyClient)
		{
			this.logger = logger;
			this.serializer = serializer;
			this.telepathyClient = telepathyClient;
		}

		public void QueueTransmission (ITransmittable message)
		{
			TransmissionQueue.Enqueue(message);
		}

		public void TransmitQueue ()
		{
			if (TransmissionQueue.Count > 0)
			{
				try
				{
					var serializedQueue = serializer.Serialize(TransmissionQueue);

					telepathyClient.Send(serializedQueue);
				}
				catch (Exception e)
				{
					logger.Error($"Failed to serialize and send transmission queue: {e.Message}");
					Console.WriteLine($"Failed to serialize and send transmission queue: {e.Message}");
				}

				TransmissionQueue.Clear();
			}
		}
	}
}
