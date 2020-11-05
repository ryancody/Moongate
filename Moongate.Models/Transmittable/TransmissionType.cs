namespace Moongate.Transmittable.Models
{
	public enum TransmissionType
	{ 
		Ping,
		PlayerConnected,
		PlayerHandshake,
		PlayerDisconnected,
		PlayerInput,
				
		EntityTransmit,
		
		GameStateRequest,
		PlayersRequest,

		Default,
		COUNT
	}
}
