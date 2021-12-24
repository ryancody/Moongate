using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Utils;
using System.Collections.Generic;
using Telepathy;
using TelepathyServer = Telepathy.Server;

namespace Moongate.Messaging.Messenger
{
	public class ServerMessenger : IMessenger
	{
		private readonly ISerializer serializer;
		private readonly TelepathyServer telepathyServer;
		private readonly PlayerStateController playerStateController;
		
		private Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ServerMessenger (ISerializer serializer,
			Common telepathyServer,
			PlayerStateController playerStateController)
		{
			this.serializer = serializer;
			this.telepathyServer = (TelepathyServer)telepathyServer;
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
