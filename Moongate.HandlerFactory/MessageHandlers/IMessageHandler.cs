using Moongate.Transmittable.Models;

namespace Moongate.HandlerFactory.MessageHandlers
{
	public interface IMessageHandler
	{
		void Handle (ITransmittable m);
	}
}
