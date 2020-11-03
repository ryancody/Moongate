using Moongate.HandlerFactory.MessageHandlers;
using Moongate.Transmittable.Models;

namespace Moongate.HandlerFactory
{
	public interface IHandlerFactory
	{
		IMessageHandler GetHandler (MessageType t);
	}
}
