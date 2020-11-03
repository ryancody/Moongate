using Moongate.Logger;

namespace Moongate.HandlerFactory.MessageHandlers
{
	public class BaseHandler
	{
		protected readonly ILogger logger;

		public BaseHandler (ILogger logger)
		{
			this.logger = logger;
		}
	}
}
