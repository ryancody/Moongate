using System;
using System.Net.WebSockets;
using CoreNET.Controllers.Messenger;
using CoreSDK.Utils;

namespace CoreSDK.Models
{
	[Serializable]
	public class Transmission : ITransmittable
	{
		public string SenderGuid { get; set; }
		public ConnectionId SenderConnectionId { get; set; }
		public long Timestamp { get; }
		public ConnectionId ToId { get; set; }
		public MessageType MessageType { get; set; }
		public object Payload { get; set; }

		public Transmission ()
		{
			Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		}
	}
}
