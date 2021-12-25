﻿using Moongate.Models.Transmittable;

namespace Moongate.Messaging.Handler
{
	public interface IHandlerProvider
	{
		ITransmissionHandler GetHandler (TransmissionType t);
		PlayerConnectedHandler PlayerConnectedHandler { get; }
		PlayerHandshakeHandler PlayerHandshakeHandler { get; }
		PlayerDisconnectedHandler PlayerDisconnectedHandler { get; }
		PingHandler PingHandler { get; }
		NetEventHandler NetEventHandler { get; }
	}
}
