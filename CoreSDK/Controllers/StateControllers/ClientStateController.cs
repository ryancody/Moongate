using CoreSDK.Models;
using System;
using System.Linq;

namespace CoreSDK.Controllers
{
	public class ClientStateController : StateController
	{
        readonly ILogger logger;

        public GameState LocalState { get; private set; }

        public static event EventHandler<BasicPlayerRequestArgs> EntityCreated;
        public static event EventHandler<BasicPlayerRequestArgs> EntityUpdated;

        public ClientStateController (ILogger l)
        {
            logger = l;

            LocalState = new GameState();

            GameStateRequestHandler.GameStateReceived += OnGameStateReceived;
            EntityHandler.EntityReceived += OnEntityReceived;
        }

        private void OnGameStateReceived (object sender, BasicPlayerRequestArgs a)
        {
            var state = (GameState)a.Payload;

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

        private void OnEntityReceived (object sender, BasicPlayerRequestArgs a)
        {
            var entity = (Entity)a.Payload;

            LocalState.Entities[entity.Guid] = entity;
        }

        public void CreateEntity (Entity e)
        {
            LocalState.Entities[e.Guid] = e;

            var args = new BasicPlayerRequestArgs()
            {
                Payload = e
            };

            EntityCreated?.Invoke(this, args);
        }

        public void UpdateEntity (Entity e)
        {
            LocalState.Entities[e.Guid] = e;

            var args = new BasicPlayerRequestArgs()
            {
                Payload = e
            };

            EntityUpdated?.Invoke(this, args);
        }
    }
}
