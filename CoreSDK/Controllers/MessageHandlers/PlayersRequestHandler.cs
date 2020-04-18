using CoreSDK.Controllers;
using CoreSDK.Models;
using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public class PlayersRequestHandler : IMessageHandler
	{
		private readonly ILogger logger;

		public static event EventHandler<PlayersRequestArgs> PlayersRequested;

		public PlayersRequestHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (Transmission message)
		{
			if (message.Payload == null)
			{
				logger.Debug("received players request");

				var requestArgs = new PlayersRequestArgs()
				{
					RequestedBy = message.SenderConnectionId
				};

				PlayersRequested?.Invoke(this, requestArgs);
			}
			else
			{
				logger.Debug("received players");

				var playersReceievedArgs = (PlayersRequestArgs)message.Payload;

				PlayersRequested?.Invoke(this, playersReceievedArgs);
			}
		}
	}
}
