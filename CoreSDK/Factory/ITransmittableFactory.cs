using CoreNET.Controllers.Messenger;
using CoreSDK.Models;

namespace CoreSDK.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (ConnectionId toId, MessageType messageType, object payload);
	}
}
