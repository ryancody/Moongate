using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Controllers
{
	public class GameStateController
	{
		readonly ILogger logger;
		readonly GameState gameState;

		public GameStateController (ILogger _logger, GameState _gameState)
		{
			logger = _logger;
			gameState = _gameState;
		}

		public Entity GetEntity (string guid)
		{
			return gameState.Entities[guid];
		}

		public void AddEntity (Entity e)
		{
			gameState.Entities.Add(e.Guid, e);
		}

		public void UpdateEntity (Entity e)
		{
			gameState.Entities[e.Guid] = e;
		}

		public bool HasEntity (Entity e)
		{
			return gameState.Entities.ContainsKey(e.Guid);
		}
	}
}
