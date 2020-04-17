using System;
using System.Net.WebSockets;
using CoreSDK.Utils;

namespace CoreSDK.Models
{
	[Serializable]
	public class Transmission : Transmittable
	{
		public string SenderGuid { get; set; }
		public int SenderConnectionId { get; set; }
		public long Timestamp { get; set; }
		public MessageType MessageType { get; set; }
		public object Payload { get; set; }

		public Transmission (MessageType messageType, object payload)
		{
			SenderGuid = LocalId.Guid;
			Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
			MessageType = messageType;
			Payload = payload;
		}
	}
}
