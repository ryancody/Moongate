using Moongate.Network.Events;
using Network.Factory;
using Network.Models;
using Network.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Telepathy;

namespace Network.Controllers
{
	public class ServerMessenger : IMessenger
	{
		readonly ILogger logger;
		readonly ISerializer serializer;
		readonly ITransmittableFactory transmittableFactory;
		readonly IHandlerFactory handlerFactory;
		readonly IMessageReceiver messageProcessor;
		readonly Telepathy.Server server;
		readonly PlayerStateController playerStateController;
		
		Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ServerMessenger (ILogger _logger,
			Telepathy.Server _server,
			PlayerStateController _playerStateController,
			IMessageReceiver _messageProcessor,
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
			((PlayerInputHandler)handlerFactory.GetHandler(MessageType.PlayerInput)).PlayerInputChanged += OnPlayerInput;
			((EntityHandler)handlerFactory.GetHandler(MessageType.EntityTransmit)).EntityReceived += OnEntityReceived;
		}

		private void OnEntityReceived (object sender, EntityArgs e)
		{
			var t = transmittableFactory.Build(MessageType.EntityTransmit, e);

			QueueTransmission(t);
		}

		private void OnPlayerInput (object sender, ControlArgs e)
		{
			try
			{
				var t = transmittableFactory.Build(MessageType.PlayerInput, e);

				QueueTransmission(t);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private void OnPingReceived (object sender, PingArgs a)
		{
			var t = transmittableFactory.Build(MessageType.Ping, a);

			QueueTransmission(t);
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs a)
		{
			var t = transmittableFactory.Build(MessageType.PlayerConnected, a);

			QueueTransmission(t);
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

				playerStateController.GetPlayers().ForEach(p => {
					server.Send(p.ConnectionId, serializedQueue);
				});

				TransmissionQueue.Clear();
			}
		}
	}
}
