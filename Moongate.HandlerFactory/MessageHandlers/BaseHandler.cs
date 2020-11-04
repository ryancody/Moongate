using Moongate.Logger;

namespace Moongate.Messaging.Handler
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
