using Network.Controllers;
using Network.Models;

namespace Network.Factory
{
	public interface IHandlerFactory
	{
		IMessageHandler GetHandler (MessageType t);
	}
}
