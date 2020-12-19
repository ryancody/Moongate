using Moongate.Logger;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TelepathyServer = Telepathy.Server;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Moongate.Messaging.Messenger.Test")]
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
			if (TransmissionQueue.Count <= 0)
			{
				return;
			}

			try
			{
				var queuesById = BuildQueues(TransmissionQueue);

				queuesById.ToList().ForEach(q => 
				{
					var serializedQueue = serializer.Serialize(q);

					playerStateController.GetPlayers().ForEach(p => 
					{
						if (q.Key == null || q.Key == p.ConnectionId)
						{
							telepathyServer.Send(p.ConnectionId, serializedQueue);
						}
					});
				});
			}
			catch (Exception e)
			{
				logger.Error($"Failed to send transmission queue: {e.Message}");
				Console.WriteLine($"Failed to send transmission queue: {e.Message}");
			}

			TransmissionQueue.Clear();
		}

		internal IEnumerable<IGrouping<int?, ITransmittable>> BuildQueues (IEnumerable<ITransmittable> queue) => queue.GroupBy(s => s.ToConnectionId);
	}
}
