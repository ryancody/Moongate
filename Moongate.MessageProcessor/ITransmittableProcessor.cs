using Moongate.Transmittable.Models;

namespace Moongate.MessageProcessor
{
	interface ITransmittableProcessor
	{
		void Process (ITransmittable t);
	}
}
