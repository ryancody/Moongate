using Moongate.Transmittable.Models;

namespace Moongate.Messaging.Handler
{
	public interface IHandlerFactory
	{
		IMessageHandler GetHandler (MessageType t);
	}
}
