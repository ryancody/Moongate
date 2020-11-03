using Moongate.Transmittable.Models;

namespace Moongate.TransmittableFactory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (MessageType messageType, object payload);
	}
}
