using System;
using Moongate.Models.Events;
using ProtoBuf;

namespace Moongate.Models.Transmittable
{
	[ProtoContract]
	public class Transmission : ITransmittable
	{
		[ProtoMember(1)]
		public string SenderGuid { get; set; }
		[ProtoMember(2)]
		public int? SenderConnectionId { get; set; }
		[ProtoMember(3)]
		public int? ToConnectionId { get; set; }
		[ProtoMember(4)]
		public long Timestamp { get; set; } = DateTimeOffset.Now.ToUnixTimeMilliseconds();
		[ProtoMember(5)]
		public TransmissionType TransmissionType { get; set; }
		[ProtoMember(6)]
		public IEventArgs Payload { get; set; }
	}
}
