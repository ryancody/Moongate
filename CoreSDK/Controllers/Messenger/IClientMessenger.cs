using CoreSDK.Models;

namespace CoreSDK.Controllers
{
	public interface IClientMessenger
	{
		void Transmit (ITransmittable t);
		void Ping ();
	}
}