using CoreSDK.Controllers;
using CoreSDK.Models;
using System;

namespace CoreSDK
{
	public class GameStateRequestHandler : IMessageHandler
	{
		private ILogger logger;

		public static event EventHandler<GameStateRequestArgs> GameStateReceived;
		public static event EventHandler<GameStateRequestArgs> GameStateRequested;

		public GameStateRequestHandler (ILogger logger)
		{
			this.logger = logger;
		}

		public void Handle (Transmission m)
		{

			if (m.Payload == null)
			{
				GameStateRequested?.Invoke();
			}
			else
			{
				GameStateReceived?.Invoke();
			}
		}
	}
}