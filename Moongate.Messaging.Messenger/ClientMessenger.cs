using Moongate.Models.Transmittable;
using Moongate.Utils;
using System.Collections.Generic;
using Telepathy;
using TelepathyClient = Telepathy.Client;

namespace Moongate.Messaging.Messenger
{
	public class ClientMessenger : IMessenger
	{
		private readonly ISerializer serializer;
		private readonly TelepathyClient telepathyClient;

		private Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ClientMessenger (ISerializer serializer,
			Common telepathyClient)
		{
			this.serializer = serializer;
			this.telepathyClient = (TelepathyClient)telepathyClient;
		}

		public void QueueTransmission (ITransmittable message)
		{
			TransmissionQueue.Enqueue(message);
		}

		public void TransmitQueue ()
		{
			if (TransmissionQueue.Count > 0)
			{
				var serializedQueue = serializer.Serialize(TransmissionQueue);

				telepathyClient.Send(serializedQueue);
				TransmissionQueue.Clear();
			}
		}
	}
}
