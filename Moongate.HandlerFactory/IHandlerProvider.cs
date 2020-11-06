using Moongate.Transmittable.Models;

namespace Moongate.Messaging.Handler
{
	public interface IHandlerProvider
	{
		ITransmissionHandler GetHandler (TransmissionType t);
	}
}
