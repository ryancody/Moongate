namespace Moongate.Transmittable.Models
{
	public interface ITransmittable
	{
		string SenderGuid { get; set; }
		int? SenderConnectionId { get; set; }
		long Timestamp { get; }
		TransmissionType TransmissionType { get; set; }
		object Payload { get; set; }
	}
}
