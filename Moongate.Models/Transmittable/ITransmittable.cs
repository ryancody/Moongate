using Moongate.Models.Events;
using ProtoBuf;

namespace Moongate.Models.Transmittable
{
	[ProtoContract]
	[ProtoInclude(100, typeof(Transmission))]
	public interface ITransmittable
	{
		[ProtoMember(1)]
		string SenderGuid { get; set; }
		[ProtoMember(2)]
		int? SenderConnectionId { get; set; }
		[ProtoMember(3)]
		int? ToConnectionId { get; set; }
		[ProtoMember(4)]
		long Timestamp { get; set; }
		[ProtoMember(5)]
		TransmissionType TransmissionType { get; set; }
		[ProtoMember(6)]
		IEventArgs Payload { get; set; }
	}
}
