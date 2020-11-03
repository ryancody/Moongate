namespace Moongate.Transmittable.Models
{
	public enum MessageType
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
