using CoreSDK.Models;
using System;
using Telepathy;
using CoreSDK;
using CoreSDK.Factory;
using CoreSDK.Utils;
using System.Collections.Generic;
using System.Linq;
using CoreNET.Controllers.Messenger;

namespace CoreSDK.Controllers
{
	public class ClientMessenger : IMessenger
	{
		readonly Client client;
		readonly ILogger logger;
		readonly ITransmittableFactory transmittableFactory;
		readonly ISerializer serializer;
		readonly GameStateController gameStateController;

		Queue<ITransmittable> transmissionQueue = new Queue<ITransmittable>();

		public ClientMessenger (ILogger _logger, Client _client, ITransmittableFactory _transmittableFactory, GameStateController _gameStateController, ISerializer _serializer)
		{
			logger = _logger;
			client = _client;
			transmittableFactory = _transmittableFactory;
			serializer = _serializer;
			gameStateController = _gameStateController;

			CoreClient.ConnectedToServer += CoreClient_ConnectedToServer;
			gameStateController.EntityAdded += OnEntityAdded;
			gameStateController.EntityUpdated += OnEntityUpdated;
		}

		private void OnEntityUpdated (object sender, EntityArgs e)
		{
			if (e.Entity.Owner == LocalId.Guid)
			{
				var t = transmittableFactory.Build(ConnectionId.Server, MessageType.EntityTransmit, e);

				QueueTransmission(t);
			}
		}

		private void OnEntityAdded (object sender, EntityArgs e)
		{
			if (e.Entity.Owner == LocalId.Guid)
			{
				var t = transmittableFactory.Build(ConnectionId.Server, MessageType.EntityTransmit, e);

				QueueTransmission(t);
			}
		}

		private void CoreClient_ConnectedToServer (object sender, EventArgs e)
		{
			InitiateHandshake();
		}

		public void InitiateHandshake ()
		{
			logger.Debug("initiating handshake");

			var args = new PlayerHandshakeArgs()
			{
				Name = LocalId.Name,
				Guid = LocalId.Guid
			};
			var t = transmittableFactory.Build(ConnectionId.Server, MessageType.PlayerHandshake, args);

			QueueTransmission(t);
		}

		public void Ping ()
		{
			var args = new PingArgs()
			{
				InitialTimestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds()
			};
			var t = transmittableFactory.Build(ConnectionId.Server, MessageType.Ping, args);

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
