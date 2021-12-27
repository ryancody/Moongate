using Moongate.Models.Transmittable;
using ProtoBuf;

namespace Moongate.Models.Events
{
	[ProtoContract]
	[ProtoInclude(100, typeof(MessageArgs))]
	[ProtoInclude(101, typeof(TransmissionArgs))]
	[ProtoInclude(102, typeof(PingArgs))]
	[ProtoInclude(103, typeof(ClientArgs))]
	[ProtoInclude(104, typeof(NetEventArgs))]
	public interface IEventArgs { }

	[ProtoContract]
	public class MessageArgs : IEventArgs
	{
		[ProtoMember(1)]
		public int FromConnectionId { get; set; }
		[ProtoMember(2)]
		public byte[] Payload { get; set; }
	}

	[ProtoContract]
	public class TransmissionArgs : IEventArgs
	{
		[ProtoMember(1)]
		public Transmission Transmission { get; set; }
	}

	[ProtoContract]
	public class PingArgs : IEventArgs
	{
		[ProtoMember(1)]
		public string InitiatorGuid { get; set; }
		[ProtoMember(2)]
		public long InitialTimestamp { get; set; }
		[ProtoMember(3)]
		public long? Ping { get; set; }
	}

	[ProtoContract]
	public class ClientArgs : IEventArgs
	{
		[ProtoMember(1)]
		public int ConnectionId { get; set; }
		[ProtoMember(2)]
		public string Guid { get; set; }
		[ProtoMember(3)]
		public string Name { get; set; }
	}

	[ProtoContract]
	public class NetEventArgs : IEventArgs
	{
		[ProtoMember(1)]
		public string EventType { get; set; }
		[ProtoMember(2)]
		public byte[] Payload { get; set; }
	}
}