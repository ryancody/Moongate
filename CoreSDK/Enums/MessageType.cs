using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public enum MessageType
	{ 
		Ping,
		PlayerConnected,
		PlayerHandshake,
		PlayerDisconnected,
		PlayerInput,
				
		EntityUpdate,
		
		GameStateRequest,
		PlayersRequest,

		Default,
		COUNT
	}
}
