using Moongate.Models.Transmittable;

namespace Moongate.Messaging.Handler
{
	public interface ITransmissionHandler
	{
		void Handle (ITransmittable t);
	}
}
