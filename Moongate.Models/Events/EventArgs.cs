using Moongate.Models.Transmittable;

namespace Moongate.Models.Events
{
	public class BaseEventArgs { }

	public class MessageArgs : BaseEventArgs
	{
		public int FromConnectionId { get; set; }
		public byte[] Payload { get; set; }
	}

	public class TransmissionArgs : BaseEventArgs
	{
		public Transmission Transmission { get; set; }
	}

	public class PingArgs : BaseEventArgs
	{
		public string InitiatorGuid { get; set; }
		public long InitialTimestamp { get; set; }
		public long? Ping { get; set; }
	}

	public class ClientArgs : BaseEventArgs
	{
		public int ConnectionId { get; set; }
		public string Guid { get; set; }
		public string Name { get; set; }
	}

	public class NetEventArgs : BaseEventArgs
	{
		public string SenderGuid { get; set; }
		public string EventType { get; set; }
		public byte[] Payload { get; set; }
	}
}