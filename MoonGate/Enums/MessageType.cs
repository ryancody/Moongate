using System;
using System.Collections.Generic;
using System.Text;

namespace Network
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
