using Moongate.Models.Transmittable;
using System;
using System.Collections.Generic;

namespace Moongate.Messaging.Handler
{
	public class HandlerProvider : IHandlerProvider
	{
		readonly Dictionary<TransmissionType, ITransmissionHandler> handlers;

		public PlayerConnectedHandler		PlayerConnectedHandler { get; }
		public PlayerHandshakeHandler		PlayerHandshakeHandler { get; }
		public PlayerDisconnectedHandler	PlayerDisconnectedHandler { get; }
		public PingHandler					PingHandler { get; }
		public NetEventHandler				NetEventHandler{ get; }

		public HandlerProvider (PlayerConnectedHandler playerConnectedHandler,
								PlayerHandshakeHandler playerHandshakeHandler,
								PlayerDisconnectedHandler playerDisconnectedHandler,
								PlayerInputHandler playerInputHandler,
								GameStateRequestHandler gameStateRequestHandler,
								EntityHandler entityHandler,
								PingHandler pingHandler)
		{
			PlayerConnectedHandler = playerConnectedHandler;
			PlayerHandshakeHandler = playerHandshakeHandler;
			PlayerDisconnectedHandler = playerDisconnectedHandler;
			PingHandler = pingHandler;
			NetEventHandler = NetEventHandler;

			handlers = new Dictionary<TransmissionType, ITransmissionHandler>
			{
				{ TransmissionType.PlayerConnected,		PlayerConnectedHandler },
				{ TransmissionType.PlayerDisconnected,  PlayerDisconnectedHandler },
				{ TransmissionType.PlayerHandshake,		PlayerHandshakeHandler },
				{ TransmissionType.Ping,				PingHandler },
				{ TransmissionType.NetEvent,			NetEventHandler }
			};
		}

		public ITransmissionHandler GetHandler (TransmissionType t)
		{
			try
			{
				return handlers[t];
			}
			catch
			{
				throw new Exception($"Transmission type not found in Handler Provider: {t}");
			}
		}
	}
}
