using Moongate.Logger;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Utils;
using System.Collections.Generic;
using TelepathyServer = Telepathy.Server;

namespace Moongate.Messaging.Messenger
{
	public class ServerMessenger : IMessenger
	{
		private readonly ILogger logger;
		private readonly ISerializer serializer;
		private readonly TelepathyServer telepathyServer;
		private readonly PlayerStateController playerStateController;
		
		private Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ServerMessenger (ILogger logger,
			ISerializer serializer,
			TelepathyServer telepathyServer,
			PlayerStateController playerStateController)
		{
			this.logger = logger;
			this.serializer = serializer;
			this.telepathyServer = telepathyServer;
			this.playerStateController = playerStateController;
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

				playerStateController.GetPlayers().ForEach(p => telepathyServer.Send(p.ConnectionId, serializedQueue));
				TransmissionQueue.Clear();
			}
		}
	}
}
