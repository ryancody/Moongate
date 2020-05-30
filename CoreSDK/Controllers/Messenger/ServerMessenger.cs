using CoreNET.Controllers.Messenger;
using CoreSDK.Factory;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Telepathy;

namespace CoreSDK.Controllers
{
	public class ServerMessenger : IMessenger
	{
		readonly ILogger logger;
		readonly ISerializer serializer;
		readonly ITransmittableFactory transmittableFactory;
		readonly IHandlerFactory handlerFactory;
		readonly IMessageProcessor messageProcessor;
		readonly Server server;
		readonly PlayerStateController playerStateController;

		Dictionary<ConnectionId, Queue<ITransmittable>> TransmissionQueue { get; set; } = new Dictionary<ConnectionId, Queue<ITransmittable>>();

		public ServerMessenger (ILogger _logger,
			Server _server,
			PlayerStateController _playerStateController,
			IMessageProcessor _messageProcessor,
			ITransmittableFactory _transmittableFactory,
			IHandlerFactory _handlerFactory,
			ISerializer _serializer)
		{
			logger = _logger;
			server = _server;
			playerStateController = _playerStateController;
			messageProcessor = _messageProcessor;
			serializer = _serializer;
			handlerFactory = _handlerFactory;
			transmittableFactory = _transmittableFactory;

			((PingHandler)handlerFactory.GetHandler(MessageType.Ping)).PingReceived += OnPingReceived;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(MessageType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
			((PlayerInputHandler)handlerFactory.GetHandler(MessageType.PlayerInput)).PlayerInput += OnPlayerInput;
			((EntityHandler)handlerFactory.GetHandler(MessageType.EntityTransmit)).EntityReceived += OnEntityReceived;
		}

		private void OnEntityReceived (object sender, EntityArgs e)
		{
			var t = transmittableFactory.Build(ConnectionId.All, MessageType.EntityTransmit, e);
			t.SenderConnectionId = playerStateController.GetPlayer(e.Entity.Owner).ConnectionId;

			QueueTransmission(t);
		}

		private void OnPlayerInput (object sender, PlayerInputArgs e)
		{
			try
			{
				var t = transmittableFactory.Build(ConnectionId.All, MessageType.PlayerInput, e);

				QueueTransmission(t);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			var t = transmittableFactory.Build(e.ConnectionId, MessageType.Ping, e);

			QueueTransmission(t);
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs a)
		{
			var t = transmittableFactory.Build(ConnectionId.All, MessageType.PlayerConnected, a);

			QueueTransmission(t);
		}

		public void QueueTransmission (ITransmittable message)
		{
			if (!TransmissionQueue.ContainsKey(message.ToId))
			{
				TransmissionQueue.Add(message.ToId, new Queue<ITransmittable>());
			}

			TransmissionQueue[message.ToId].Enqueue(message);
		}

		public void TransmitQueue ()
		{
			TransmissionQueue.Keys.ToList().ForEach(k =>
			{
				if (TransmissionQueue[k].Count > 0)
				{
					var tSer = serializer.Serialize(TransmissionQueue[k]);

					if (k == ConnectionId.All)
					{
						playerStateController.GetPlayers().ForEach(p =>
						{
							server.Send((int)p.ConnectionId, tSer);
						});
					}
					else
					{
						server.Send((int)k, tSer);
					}

					TransmissionQueue[k].Clear();
				}
			});
		}
	}
}
