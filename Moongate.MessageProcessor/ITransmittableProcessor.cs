using Moongate.Transmittable.Models;

namespace Moongate.TransmittableProcessor
{
	interface ITransmittableProcessor
	{
		void Process (ITransmittable t);
	}
}
