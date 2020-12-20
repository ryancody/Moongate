using Moongate.Models.Events;

namespace Moongate.Models.Transmittable
{
	public interface ITransmittable
	{
		string SenderGuid { get; set; }
		int? SenderConnectionId { get; set; }
		int? ToConnectionId { get; set; }
		long Timestamp { get; set; }
		TransmissionType TransmissionType { get; set; }
		BaseEventArgs Payload { get; set; }
	}
}
