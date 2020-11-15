namespace Moongate.Models.Transmittable
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
