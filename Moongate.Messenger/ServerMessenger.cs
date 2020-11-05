﻿using Moongate.HandlerFactory;
using Moongate.HandlerFactory.MessageHandlers;
using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.StateController;
using Moongate.Transmittable.Models;
using Moongate.TransmittableFactory;
using Moongate.Utils;
using System;
using System.Collections.Generic;
using TelepathyServer = Telepathy.Server;

namespace Moongate.Messenger
{
	public class ServerMessenger : IMessenger
	{
		private readonly ILogger logger;
		private readonly ISerializer serializer;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly TelepathyServer telepathyServer;
		private readonly PlayerStateController playerStateController;
		
		Queue<ITransmittable> TransmissionQueue { get; set; } = new Queue<ITransmittable>();

		public ServerMessenger (ILogger logger,
			ISerializer serializer,
			TelepathyServer telepathyServer,
			PlayerStateController playerStateController,
			ITransmittableFactory transmittableFactory,
			IHandlerFactory handlerFactory)
		{
			this.logger = logger;
			this.serializer = serializer;
			this.telepathyServer = telepathyServer;
			this.playerStateController = playerStateController;
			this.transmittableFactory = transmittableFactory;

			((PingHandler)handlerFactory.GetHandler(TransmissionType.Ping)).PingReceived += OnPingReceived;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(TransmissionType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
			((PlayerInputHandler)handlerFactory.GetHandler(TransmissionType.PlayerInput)).PlayerInputChanged += OnPlayerInput;
			((EntityHandler)handlerFactory.GetHandler(TransmissionType.EntityTransmit)).EntityReceived += OnEntityReceived;
		}

		private void OnEntityReceived (object sender, EntityArgs e)
		{
			var t = transmittableFactory.Build(TransmissionType.EntityTransmit, e);

			QueueTransmission(t);
		}

		private void OnPlayerInput (object sender, ControlArgs e)
		{
			try
			{
				var t = transmittableFactory.Build(TransmissionType.PlayerInput, e);

				QueueTransmission(t);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		private void OnPingReceived (object sender, PingArgs a)
		{
			var t = transmittableFactory.Build(TransmissionType.Ping, a);

			QueueTransmission(t);
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs a)
		{
			var t = transmittableFactory.Build(TransmissionType.PlayerConnected, a);

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

				playerStateController.GetPlayers().ForEach(p => telepathyServer.Send(p.ConnectionId, serializedQueue));
				TransmissionQueue.Clear();
			}
		}
	}
}
