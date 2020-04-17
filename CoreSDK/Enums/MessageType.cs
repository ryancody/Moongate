using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public enum MessageType
	{ 
		CreateEntity,
		Ping,
		Pong,
		PlayerConnected,
		PlayerHandshake,
		PlayerDisconnected,
		UpdateEntity,
		RequestPlayersList,
		GameStateRequest,

		Default,
		COUNT
	}
}
