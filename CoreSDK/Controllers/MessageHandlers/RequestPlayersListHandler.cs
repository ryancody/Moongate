using CoreSDK.Controllers;
using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public class RequestPlayersListHandler : IMessageHandler
	{
		public static event EventHandler<BasicPlayerRequestArgs> PlayersListRequest;

		public RequestPlayersListHandler ()
		{
			PlayersListRequest += new EventHandler<BasicPlayerRequestArgs>(OnPlayersListRequested);
		}

		public void Handle (Transmission message)
		{
			var handshakeArgs = new BasicPlayerRequestArgs()
			{
				ConnectionId = message.SenderConnectionId,
				ClientGuid = message.SenderGuid,
				Payload = message.Payload
			};

			PlayersListRequest?.Invoke(this, handshakeArgs);
		}

		protected virtual void OnPlayersListRequested (object sender, BasicPlayerRequestArgs e)
		{
		
		}
	}
}
