using Moongate.Messaging.Handler;
using Moongate.State.Models;

namespace Moongate.State.Controller
{
	public class GameStateController
	{

		public GameState GameState { get; set; }

		public GameStateController (GameState gamestate, IHandlerProvider handlerProvider)
		{
			GameState = gamestate;
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
		}

		public void UpdateEntity (Entity e)
		{
			GameState.Entities[e.Guid] = e;
		}

		public bool HasEntity (Entity e)
		{
			return GameState.Entities.ContainsKey(e.Guid);
		}
	}
}
