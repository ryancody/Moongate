using Moongate.Logger;
using Moongate.Models.Events;
using Moongate.Transmittable.Models;
using System;

namespace Moongate.HandlerFactory.MessageHandlers
{
	public class PlayerConnectedHandler : BaseHandler, IMessageHandler
	{
		public event EventHandler<PlayerConnectionArgs> PlayerConnected;

		public PlayerConnectedHandler (ILogger logger) : base(logger) { }

		public void Handle (ITransmittable message)
		{
			var playerConnectionArgs = new PlayerConnectionArgs()
			{
				ConnectionId = message.SenderConnectionId.GetValueOrDefault()
			};

			PlayerConnected?.Invoke(this, playerConnectionArgs);
		}
	}
}
