using Moongate.Logger;
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

		public HandlerProvider (ILogger logger)
		{
			PlayerConnectedHandler = new PlayerConnectedHandler(logger);
			PlayerHandshakeHandler = new PlayerHandshakeHandler(logger);
			PlayerDisconnectedHandler = new PlayerDisconnectedHandler(logger);
			PingHandler = new PingHandler(logger);
			NetEventHandler = new NetEventHandler(logger);

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
