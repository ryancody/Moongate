using CoreNET.Controllers.Messenger;
using System.Collections.Generic;
namespace CoreSDK
{
	public class PlayerState
	{
		// GUID to Player
		public Dictionary<string, Player> Players { get; set; }

		// ConnectionId to GUID
		public Dictionary<ConnectionId, string> GUIDs { get; set; }

		public PlayerState ()
		{
			Players = new Dictionary<string, Player>();
			GUIDs = new Dictionary<ConnectionId, string>();
		}
	}
}
