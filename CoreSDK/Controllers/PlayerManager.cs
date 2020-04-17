using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace CoreSDK
{
	public class PlayerManager
	{
		// GUID to Player
		public Dictionary<string, Player> Players { get; set; }

		// ConnectionId to GUID
		public Dictionary<int, string> GUIDs { get; set; }

		public PlayerManager ()
		{
			Players = new Dictionary<string, Player>();
			GUIDs = new Dictionary<int, string>();
		}

		public Player GetPlayer (string guid)
		{
			return Players[guid];
		}

		public Player GetPlayer (int connectionId)
		{
			return Players[GetGuid(connectionId)];
		}

		public string GetGuid (int connectionId)
		{
			return GUIDs[connectionId];
		}

		public void AddPlayer (int connectionId, string guid, string name)
		{
			var player = new Player()
			{
				ConnectionId = connectionId,
				GUID = guid,
				Name = name,
			};

			if (Players.ContainsKey(guid))
			{
				Players[guid] = player;
				GUIDs[connectionId] = guid;
			}
			else
			{ 
				Players.Add(guid, player);
				GUIDs.Add(connectionId, guid);
			}
		}

		public void RemovePlayer (int connectionId)
		{
			var guid = GetGuid(connectionId);

			Players.Remove(guid);
			GUIDs.Remove(connectionId);
		}
	}
}
