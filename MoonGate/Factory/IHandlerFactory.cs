using CoreSDK.Controllers;
using CoreSDK.Models;

namespace CoreSDK.Factory
{
	public interface IHandlerFactory
	{
		IMessageHandler GetHandler (MessageType t);
	}
}
