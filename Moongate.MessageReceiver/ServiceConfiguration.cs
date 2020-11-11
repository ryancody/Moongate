using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Moongate.Messaging.Listener;
using Moongate.Utils;

namespace Moongate.Messaging.Receiver
{
	public static class ServiceConfiguration
	{
		public static void AddMessageReceiver (this IServiceCollection services)
		{
			services.AddSingleton<IMessageReceiver, MessageReceiver>(s => 
			{
				var logger = s.GetRequiredService<ILogger>();
				var serializer = s.GetRequiredService<ISerializer>();
				var messageListener = s.GetRequiredService<IMessageListener>();

				return new MessageReceiver(logger, serializer, messageListener);
			});
		}
	}
}
