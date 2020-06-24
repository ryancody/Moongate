namespace CoreSDK.Models
{

	public interface ITransmittable
	{
		string SenderGuid { get; set; }
		int? SenderConnectionId { get; set; }
		long Timestamp { get; }
		MessageType MessageType { get; set; }
		object Payload { get; set; }
	}
}
