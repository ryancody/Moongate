using CoreSDK.Models;

namespace CoreSDK.Controllers
{
	public interface IServerMessenger
	{
		void Transmit (int connectionId, Transmission message);
	}
}