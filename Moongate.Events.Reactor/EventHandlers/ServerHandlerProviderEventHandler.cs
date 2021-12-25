using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Messenger;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using Moongate.State.Controller;
using Moongate.Transmittable.Factory;

namespace Moongate.Events.Reactor.EventHandlers
{
	public class ServerHandlerProviderEventHandler : IEventHandler
	{
		private readonly IMessenger messenger;
		private readonly ITransmittableFactory transmittableFactory;
		private readonly GameStateController gameStateController;
		private readonly PlayerStateController playerStateController;

		public ServerHandlerProviderEventHandler (IHandlerProvider handlerProvider, 
			IMessenger messenger, 
			ITransmittableFactory transmittableFactory, 
			IIdentityProvider identityProvider,
			GameStateController gameStateController,
			PlayerStateController playerStateController)
		{
			this.messenger = messenger;
			this.transmittableFactory = transmittableFactory;
			this.gameStateController = gameStateController;
			this.playerStateController = playerStateController;

			handlerProvider.PingHandler.PingReceived += OnPingReceived;
			handlerProvider.PlayerHandshakeHandler.PlayerHandshake += OnPlayerHandshake;
			handlerProvider.NetEventHandler.NetEventReceived += OnNetEvent;
		}

		/// <summary>
		/// On receipt of a player handshake message.
		/// Server notifies other clients of new client's info
		/// with a player connected message. 
		/// Only server should receieve player handshake event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="newPlayer"></param>
		private void OnPlayerHandshake (object sender, ClientArgs newPlayer)
		{
			playerStateController.AddOrUpdatePlayer(newPlayer.ConnectionId, newPlayer.Guid, newPlayer.Name);

			playerStateController.GetPlayers().ForEach(p =>
			{
				var player = new ClientArgs
				{
					ConnectionId = p.ConnectionId,
					Guid = p.Guid,
					Name = p.Name
				};

				var playerStateUpdate = transmittableFactory.Build(newPlayer.ConnectionId, TransmissionType.PlayerConnected, player);
				messenger.QueueTransmission(playerStateUpdate);
			});

			var transmission = transmittableFactory.Build(null, TransmissionType.PlayerConnected, newPlayer);
			messenger.QueueTransmission(transmission);

			Console.WriteLine($"player connected: {newPlayer.Name} - {newPlayer.Guid}");
			logger.Info($"player connected: {newPlayer.Name} - {newPlayer.Guid}");
		}

		private void OnPingReceived (object sender, PingArgs e)
		{
			var transmission = transmittableFactory.Build(null, TransmissionType.Ping, e);

			messenger.QueueTransmission(transmission);
		}

		private void OnNetEvent (object sender, NetEventArgs e)
		{
			var transmission = transmittableFactory.Build(null, TransmissionType.NetEvent, e);

			messenger.QueueTransmission(transmission);
		}
	}
}
