using Moongate.Logger;
using Moongate.Transmittable.Models;
using System;
using System.Collections.Generic;

namespace Moongate.Messaging.Handler
{
	class HandlerProvider : IHandlerProvider
	{
		readonly Dictionary<TransmissionType, ITransmissionHandler> handlers;

		public PlayerConnectedHandler	PlayerConnectedHandler { get; }
		public PlayerHandshakeHandler	PlayerHandshakeHandler { get; }
		public PlayerInputHandler		PlayerInputHandler { get; }
		public GameStateRequestHandler	GameStateRequestHandler { get; }
		public EntityHandler			EntityHandler { get; }
		public PingHandler				PingHandler { get; }

		public HandlerProvider (ILogger logger)
		{
			PlayerConnectedHandler = new PlayerConnectedHandler(logger);
			PlayerHandshakeHandler = new PlayerHandshakeHandler(logger);
			PlayerInputHandler = new PlayerInputHandler(logger);
			GameStateRequestHandler = new GameStateRequestHandler(logger);
			EntityHandler = new EntityHandler(logger);
			PingHandler = new PingHandler(logger);

			handlers = new Dictionary<TransmissionType, ITransmissionHandler>
			{
				{ TransmissionType.PlayerConnected,		PlayerConnectedHandler },
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
				throw new Exception("Transmission type not found in Handler Provider: " + t);
			}
		}
	}
}
