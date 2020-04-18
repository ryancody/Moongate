using CoreSDK.Controllers;
using CoreSDK.Models;

namespace CoreSDK
{
	public class UpdateEntityHandler : IMessageHandler
	{
		private readonly ILogger logger;
		public UpdateEntityHandler (ILogger l)
		{
			logger = l;
		}

		public void Handle (Transmission m)
		{
			throw new System.NotImplementedException();
		}
	}
}