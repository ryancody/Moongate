using CoreSDK.Models;

namespace CoreSDK.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (MessageType messageType, object payload);
	}
}
