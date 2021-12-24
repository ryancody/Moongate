using Microsoft.Extensions.DependencyInjection;

namespace Moongate.Messaging.Receiver
{
	public static class ServiceConfiguration
	{
		public static void AddReceiverService(this IServiceCollection services)
		{
			services.AddSingleton<IMessageReceiver, MessageReceiver>();
		}
	}
}
