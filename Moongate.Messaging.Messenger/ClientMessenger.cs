using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.StateController;
using Moongate.Transmittable.Factory;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using TelepathyClient = Telepathy.Client;

namespace Moongate.Messaging.Messenger
{
	public class ClientMessenger : IMessenger
	{
		private readonly TelepathyClient client;
		private readonly ILogger logger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly ISerializer serializer;
		private readonly GameStateController gameStateController;
		private readonly IIdentityProvider identityProvider;

		private Queue<ITransmittable> transmissionQueue = new Queue<ITransmittable>();

		public ClientMessenger (ILogger logger, 
								TelepathyClient client, 
								ITransmittableFactory transmittableFactory, 
								GameStateController gameStateController, 
								ISerializer serializer,
								IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.client = client;
			this.transmittableFactory = transmittableFactory;
			this.serializer = serializer;
			this.gameStateController = gameStateController;
			this.identityProvider = identityProvider;

			// Client.ConnectedToServer += CoreClient_ConnectedToServer;
			gameStateController.EntityAdded += OnEntityAdded;
			gameStateController.EntityUpdated += OnEntityUpdated;
		}

		private void OnEntityUpdated (object sender, EntityArgs e)
		{
			if (e.Entity.Owner == identityProvider.Id.Guid)
			{
				var t = transmittableFactory.Build(TransmissionType.EntityTransmit, e);

				QueueTransmission(t);
			}
		}

		private void OnEntityAdded (object sender, EntityArgs e)
		{
			if (e.Entity.Owner == identityProvider.Id.Guid)
			{
				var t = transmittableFactory.Build(TransmissionType.EntityTransmit, e);

				QueueTransmission(t);
			}
		}

		//private void CoreClient_ConnectedToServer (object sender, EventArgs e)
		//{
		//	logger.Debug("initiating handshake");

		//	var args = new PlayerHandshakeArgs()
		//	{
		//		Name = LocalId.Name,
		//		Guid = LocalId.Guid
		//	};
		//	var t = transmittableFactory.Build(MessageType.PlayerHandshake, args);

		//	QueueTransmission(t);
		//}

		public void Ping ()
		{
			var args = new PingArgs()
			{
				InitialTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
			};
			var t = transmittableFactory.Build(TransmissionType.Ping, args);

			QueueTransmission(t);
		}

		public void QueueTransmission (ITransmittable message)
		{
			transmissionQueue.Enqueue(message);
		}

		public void TransmitQueue ()
		{
			if (transmissionQueue.Count > 0)
			{
				var serTransmit = serializer.Serialize(transmissionQueue);

				logger.Debug($"Sending {serTransmit.Count()} messages");

				client.Send(serTransmit);

				transmissionQueue.Clear();
			}
		}
	}
}
