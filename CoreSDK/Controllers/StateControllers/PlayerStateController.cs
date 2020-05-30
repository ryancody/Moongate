using CoreNET.Controllers.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSDK.Controllers
{
	public class PlayerStateController
	{
		readonly ILogger logger;
		readonly PlayerState playerState;

		public PlayerStateController (ILogger _logger, PlayerState _playerState)
		{
			logger = _logger;
			playerState = _playerState;
		}

		public List<Player> GetPlayers ()
		{
			return playerState.Players.Values.ToList();
		}

		public Player GetPlayer (string guid)
		{
			if (playerState.Players.ContainsKey(guid))
			{
				return playerState.Players[guid];
			}
			else
			{
				return null;
			}
		}

		public Player GetPlayer (ConnectionId connectionId)
		{
			if (GetPlayerGuid(connectionId) != null && playerState.Players.ContainsKey(GetPlayerGuid(connectionId)))
			{
				return playerState.Players[GetPlayerGuid(connectionId)];
			}
			else
			{
				return null;
			}
		}

		public string GetPlayerGuid (ConnectionId connectionId)
		{
			if(playerState.GUIDs.ContainsKey(connectionId))
			{ 
				return playerState.GUIDs[connectionId];
			}
			else
			{
				return null;
			}
		}

		public void AddPlayer (ConnectionId connectionId, string guid, string name)
		{
			var player = new Player()
			{
				ConnectionId = connectionId,
				GUID = guid,
				Name = name,
			};

			if (playerState.Players.ContainsKey(guid))
			{
				playerState.Players[guid] = player;
				playerState.GUIDs[connectionId] = guid;
			}
			else
			{
				playerState.Players.Add(guid, player);
				playerState.GUIDs.Add(connectionId, guid);
			}
		}

		public void RemovePlayer (ConnectionId connectionId)
		{
			var guid = GetPlayerGuid(connectionId);

			playerState.Players.Remove(guid);
			playerState.GUIDs.Remove(connectionId);
		}
	}
}
