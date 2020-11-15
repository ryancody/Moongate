using Moongate.Models.Transmittable;

namespace Moongate.Transmittable.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (TransmissionType messageType, object payload);
	}
}
