using Moongate.Network.Events;
using Network.Models;
using System;

namespace Moongate.StateController
{
	public class GameStateController
	{
		readonly ILogger logger;

		public GameState GameState { get; }

		public EventHandler<EntityArgs> EntityAdded { get; set; }
		public EventHandler<EntityArgs> EntityUpdated { get; set; }

		public GameStateController (ILogger _logger, GameState _gameState)
		{
			logger = _logger;
			GameState = _gameState;
		}

		public Entity GetEntity (string guid)
		{
			return GameState.Entities[guid];
		}

		public void ProcessEntity (Entity e)
		{
			if (HasEntity(e))
			{
				UpdateEntity(e);
			}
			else
			{
				AddEntity(e);
			}
		}

		public void AddEntity (Entity e)
		{
			GameState.Entities.Add(e.Guid, e);

			var args = new EntityArgs()
			{
				Entity = e
			};

			EntityAdded?.Invoke(this, args);
		}

		public void UpdateEntity (Entity e)
		{
			GameState.Entities[e.Guid] = e;

			var args = new EntityArgs()
			{
				Entity = e
			};

			EntityUpdated?.Invoke(this, args);
		}

		public bool HasEntity (Entity e)
		{
			return GameState.Entities.ContainsKey(e.Guid);
		}
	}
}
