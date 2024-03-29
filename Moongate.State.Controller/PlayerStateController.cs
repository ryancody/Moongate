﻿using Microsoft.Extensions.Logging;
using Moongate.State.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moongate.State.Controller
{
	public class PlayerStateController
	{
		private readonly ILogger<PlayerStateController> logger;
		private readonly PlayerState playerState;

		public PlayerStateController (ILogger<PlayerStateController> logger, PlayerState playerState)
		{
			this.logger = logger;
			this.playerState = playerState;
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
				logger.LogError($@"player guid {guid} not found!");
				throw new Exception("Player guid key not found!");
			}
		}

		public Player GetPlayer (int connectionId)
		{
			if (playerState.GUIDs.ContainsKey(connectionId))
			{
				return GetPlayer(playerState.GUIDs[connectionId]);
			}
			else
			{
				logger.LogError($@"player connectionId {connectionId} not found!");
				throw new Exception("Player connectionId key not found!");
			}
		}

		/// <summary>
		/// Add a player to the player list.
		/// </summary>
		/// <param name="connectionId"></param>
		/// <param name="guid"></param>
		/// <param name="name"></param>
		public void AddOrUpdatePlayer (int connectionId, string guid, string name)
		{
			var player = new Player()
			{
				ConnectionId = connectionId,
				Guid = guid,
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

		public void RemovePlayer (int connectionId)
		{
			var guid = GetPlayer(connectionId).Guid;

			playerState.GUIDs.Remove(connectionId);
			playerState.Players.Remove(guid);
		}
	}
}
