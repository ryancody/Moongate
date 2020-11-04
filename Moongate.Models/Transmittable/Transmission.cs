﻿using System;

namespace Moongate.Transmittable.Models
{
	[Serializable]
	public class Transmission : ITransmittable
	{
		public string SenderGuid { get; set; }
		public int? SenderConnectionId { get; set; }
		public long Timestamp { get; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		public MessageType MessageType { get; set; }
		public object Payload { get; set; }
	}
}
