namespace Moongate.Models.Transmittable
{
	public enum TransmissionType
	{ 
		Ping,
		PlayerConnected,
		PlayerHandshake,
		PlayerDisconnected,
						
		GameStateRequest,
		PlayersRequest,

		NetEvent,

		Default,
		COUNT
	}
}
