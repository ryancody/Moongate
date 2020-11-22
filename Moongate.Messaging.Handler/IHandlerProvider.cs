using Moongate.Models.Transmittable;

namespace Moongate.Messaging.Handler
{
	public interface IHandlerProvider
	{
		ITransmissionHandler GetHandler (TransmissionType t);
		PlayerConnectedHandler PlayerConnectedHandler { get; }
		PlayerHandshakeHandler PlayerHandshakeHandler { get; }
		PlayerDisconnectedHandler PlayerDisconnectedHandler { get; }
		PlayerInputHandler PlayerInputHandler { get; }
		GameStateRequestHandler GameStateRequestHandler { get; }
		EntityHandler EntityHandler { get; }
		PingHandler PingHandler { get; }
	}
}
