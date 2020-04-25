using CoreSDK.Models;

namespace CoreSDK.Controllers
{
	public interface IServerMessenger
	{
		void Transmit (int connectionId, ITransmittable message);
		void Broadcast (ITransmittable message);
		void Relay (ITransmittable message, string guid);
	}
}