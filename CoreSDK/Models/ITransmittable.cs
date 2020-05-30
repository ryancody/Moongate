using CoreNET.Controllers.Messenger;

namespace CoreSDK.Models
{

	public interface ITransmittable
	{
		string SenderGuid { get; set; }
		ConnectionId SenderConnectionId { get; set; }
		long Timestamp { get; }
		ConnectionId ToId { get; set; }
		MessageType MessageType { get; set; }
		object Payload { get; set; }
	}
}
