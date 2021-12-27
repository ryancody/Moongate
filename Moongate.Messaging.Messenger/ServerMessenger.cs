using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using Telepathy;
using System.Linq;
using TelepathyServer = Telepathy.Server;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

[assembly: InternalsVisibleTo("Moongate.Messaging.Messenger.Test")]
namespace Moongate.Messaging.Messenger
{
	public class ServerMessenger : IMessenger
	{
		private readonly ILogger<ServerMessenger> logger;
		private readonly ISerializer serializer;
		private readonly TelepathyServer telepathyServer;
		private readonly PlayerStateController playerStateController;

		private Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ServerMessenger (ILogger<ServerMessenger> logger,
			ISerializer serializer,
			Common telepathyServer,
			PlayerStateController playerStateController)
		{
			this.logger = logger;
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
			if (TransmissionQueue.Count <= 0 
				|| playerStateController.GetPlayers().Count <= 0) 
					return;

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
				logger.LogError($"Failed to send transmission queue: {e.Message}");
				Console.WriteLine($"Failed to send transmission queue: {e.Message}");
			}

			TransmissionQueue.Clear();
		}

		internal IEnumerable<IGrouping<int?, ITransmittable>> BuildQueues (IEnumerable<ITransmittable> queue) => queue.GroupBy(s => s.ToConnectionId);
	}
}
