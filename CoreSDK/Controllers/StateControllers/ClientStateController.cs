using CoreSDK.Models;
using System;
using System.Linq;

namespace CoreSDK.Controllers
{
	public class ClientStateController : IStateController
	{
        readonly ILogger logger;

        public GameState GameState { get; private set; }
        public PlayerState PlayerState { get; private set; }

        public static event EventHandler<EntityArgs> EntityCreated;
        public static event EventHandler<EntityArgs> EntityUpdated;

        public ClientStateController (ILogger l)
        {
            logger = l;

            GameState = new GameState();

            // TODO:  fix these listeners up

            // find away to expose events to outside of the core without exposing handler factory?
            // extend it? wrap it?

            GameStateRequestHandler.GameStateReceived += OnGameStateReceived;
            //EntityHandler.EntityReceived += OnEntityReceived;
            //PlayerConnectedHandler.PlayerStateUpdated += OnPlayerStateUpdated;
        }

        private void OnPlayerStateUpdated (object sender, PlayerStateArgs args)
        {
            PlayerState = args.PlayerState;
        }

        private void OnGameStateReceived (object sender, GameStateRequestArgs args)
        {
            var state = args.GameState;

            state.Entities.Keys.ToList().ForEach(k =>
            {
                logger.Debug("adding entity: " + state.Entities[k].Guid);

                if (GameState.Entities.ContainsKey(k))
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
            if (GameState.Entities.TryGetValue(a.Entity.Guid, out var entity))
            {
                UpdateEntity(entity);
            }
            else
            {
                CreateEntity(a.Entity);
            }
        }

        public void CreateEntity (Entity e)
        {
            GameState.Entities[e.Guid] = e;

            var args = new EntityArgs()
            {
                Entity = e
            };

            EntityCreated?.Invoke(this, args);
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
    }
}
