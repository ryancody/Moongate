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
		public PlayerInputHandler			PlayerInputHandler { get; }
		public GameStateRequestHandler		GameStateRequestHandler { get; }
		public EntityHandler				EntityHandler { get; }
		public PingHandler					PingHandler { get; }

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
			PlayerInputHandler = playerInputHandler;
			GameStateRequestHandler = gameStateRequestHandler;
			EntityHandler = entityHandler;
			PingHandler = pingHandler;

			handlers = new Dictionary<TransmissionType, ITransmissionHandler>
			{
				{ TransmissionType.PlayerConnected,		PlayerConnectedHandler },
				{ TransmissionType.PlayerDisconnected,  PlayerDisconnectedHandler },
				{ TransmissionType.PlayerHandshake,		PlayerHandshakeHandler },
				{ TransmissionType.PlayerInput,			PlayerInputHandler },
				{ TransmissionType.GameStateRequest,	GameStateRequestHandler },
				{ TransmissionType.EntityTransmit,		EntityHandler },
				{ TransmissionType.Ping,				PingHandler }
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
