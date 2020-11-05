using Moongate.Transmittable.Models;

namespace Moongate.TransmittableFactory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (TransmissionType messageType, object payload);
	}
}
