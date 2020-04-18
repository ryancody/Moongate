using CoreSDK.Models;
using System;
using System.Linq;

namespace CoreSDK.Controllers
{
	public class ClientStateController : IStateController
	{
        readonly ILogger logger;

        public GameState LocalState { get; private set; }

        public static event EventHandler<EntityArgs> EntityCreated;
        public static event EventHandler<EntityArgs> EntityUpdated;

        public ClientStateController (ILogger l)
        {
            logger = l;

            LocalState = new GameState();

            GameStateRequestHandler.GameStateReceived += OnGameStateReceived;
            EntityHandler.EntityReceived += OnEntityReceived;
        }

        private void OnGameStateReceived (object sender, GameStateRequestArgs args)
        {
            var state = args.GameState;

            state.Entities.Keys.ToList().ForEach(k =>
            {
                logger.Debug("adding entity: " + state.Entities[k].Guid);

                if (LocalState.Entities.ContainsKey(k))
                {
                    UpdateEntity(state.Entities[k]);
                }
                else
                {
                    CreateEntity(state.Entities[k]);
                }
            });
        }

        private void OnEntityReceived (object sender, EntityArgs a)
        {
            LocalState.Entities[a.Entity.Guid] = a.Entity;
        }

        public void CreateEntity (Entity e)
        {
            LocalState.Entities[e.Guid] = e;

            var args = new EntityArgs()
            {
                Entity = e
            };

            EntityCreated?.Invoke(this, args);
        }

        public void UpdateEntity (Entity e)
        {
            LocalState.Entities[e.Guid] = e;

            var args = new EntityArgs()
            {
                Entity = e
            };

            EntityUpdated?.Invoke(this, args);
        }
    }
}
