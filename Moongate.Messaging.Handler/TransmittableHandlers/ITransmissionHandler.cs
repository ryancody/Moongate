using Moongate.Transmittable.Models;

namespace Moongate.Messaging.Handler
{
	public interface ITransmissionHandler
	{
		void Handle (ITransmittable t);
	}
}
