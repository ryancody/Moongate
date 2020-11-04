using Moongate.Transmittable.Models;

namespace Moongate.Messaging.Handler
{
	public interface IMessageHandler
	{
		void Handle (ITransmittable m);
	}
}
