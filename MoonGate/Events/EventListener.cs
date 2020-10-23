using Network;
using Network.Controllers;
using Network.Factory;

namespace Moongate.Network.Events
{
	public class EventListener
	{
		readonly ILogger logger;
		readonly ITransmittableFactory transmittableFactory;
		readonly IMessenger messenger;
		readonly GameStateController gameStateController;

		public EventListener (ILogger _logger, ITransmittableFactory _transmittableFactory, IMessenger _messenger, GameStateController _gameStateController) 
		{
			logger = _logger;
			transmittableFactory = _transmittableFactory;
			messenger = _messenger;
			gameStateController = _gameStateController;
		}

		public void OnPlayerInputChanged (object sender, ControlArgs e)
		{
			var t = transmittableFactory.Build(MessageType.PlayerInput, e);
			messenger.QueueTransmission(t);
		}

		public void OnEntityUpdate (object sender, EntityArgs e)
		{
			var t = transmittableFactory.Build(MessageType.EntityTransmit, e);
			messenger.QueueTransmission(t);
		}
	}
}
