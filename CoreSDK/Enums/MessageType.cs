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
				
		CreateEntity,
		UpdateEntity,
		
		GameStateRequest,
		PlayersRequest,

		Default,
		COUNT
	}
}
