using Moongate.Transmittable.Models;

namespace Moongate.MessageProcessor
{
	interface IMessageProcessor
	{
		void Process (ITransmittable t);
	}
}
