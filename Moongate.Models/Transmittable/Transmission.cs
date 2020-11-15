using System;

namespace Moongate.Models.Transmittable
{
	[Serializable]
	public class Transmission : ITransmittable
	{
		public string SenderGuid { get; set; }
		public int? SenderConnectionId { get; set; }
		public long Timestamp { get; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		public TransmissionType TransmissionType { get; set; }
		public object Payload { get; set; }
	}
}
