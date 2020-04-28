using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace CoreSDK
{
	public class PlayerState
	{
		// GUID to Player
		public Dictionary<string, Player> Players { get; set; }

		// ConnectionId to GUID
		public Dictionary<int, string> GUIDs { get; set; }

		public PlayerState ()
		{
			Players = new Dictionary<string, Player>();
			GUIDs = new Dictionary<int, string>();
		}
	}
}
