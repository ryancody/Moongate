using Moongate.Models.Events;
using System;

namespace Moongate.Models.Transmittable
{
	public class Transmission : ITransmittable
	{
		public string SenderGuid { get; set; }
		public int? SenderConnectionId { get; set; }
		public int? ToConnectionId { get; set; }
		public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		public TransmissionType TransmissionType { get; set; }
		public BaseEventArgs Payload { get; set; }
	}
}
