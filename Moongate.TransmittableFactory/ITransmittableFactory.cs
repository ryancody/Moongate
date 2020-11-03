using Network.Models;

namespace Network.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (MessageType messageType, object payload);
	}
}
