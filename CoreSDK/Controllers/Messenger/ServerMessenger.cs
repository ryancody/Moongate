using CoreSDK.Factory;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Telepathy;

namespace CoreSDK.Controllers
{
	public class ServerMessenger : IServerMessenger
	{
		readonly ILogger logger;
		readonly ISerializer serializer;
		readonly ITransmittableFactory transmittableFactory;
		readonly IHandlerFactory handlerFactory;
		readonly Server server;
		readonly PlayerState playerState;

		public ServerMessenger (ILogger _logger, Server _server, PlayerState _playerState, ITransmittableFactory _transmittableFactory, IHandlerFactory _handlerFactory, ISerializer _serializer)
		{
			logger = _logger;
			server = _server;
			playerState = _playerState;
			serializer = _serializer;
			handlerFactory = _handlerFactory;
			transmittableFactory = _transmittableFactory;

			((PingHandler)handlerFactory.GetHandler(MessageType.Ping)).PingReceived += OnPingReceived;
			((PlayerHandshakeHandler)handlerFactory.GetHandler(MessageType.PlayerHandshake)).PlayerHandshake += OnPlayerHandshake;
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			var t = transmittableFactory.Build(MessageType.Ping, e);

			Transmit(e.ConnectionId, t);
		}

		private void OnPlayerHandshake (object sender, PlayerHandshakeArgs a)
		{
			var t = transmittableFactory.Build(MessageType.PlayerConnected, a);

			Broadcast(t);
		}

		public void Broadcast (ITransmittable t)
		{

			playerState.Players.Values.ToList().ForEach(p =>
			{
				Transmit(p.ConnectionId, t);
			});
		}

		public void Relay (ITransmittable t, string guid)
		{
			var toId = playerState.GetPlayer(guid).ConnectionId;

			Transmit(toId, t);
		}

		public void Transmit (int connectionId, ITransmittable message)
		{
			logger.Debug("Send - connectonId: " + connectionId + " - messageType: " + message.MessageType.ToString());

			var bytes = serializer.Serialize(message);

			server.Send(connectionId, bytes);
		}
	}
}
