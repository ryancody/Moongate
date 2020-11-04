using Microsoft.Extensions.DependencyInjection;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Receiver;

namespace Moongate.TransmittableProcessor
{
	public static class ServiceConfiguration
	{
		public static void AddTransmittableProcessor (this IServiceCollection services)
		{
			services.AddSingleton<ITransmittableProcessor, TransmittableProcessor>(s =>
			{
				var logger = s.GetRequiredService<ILogger>();
				var messageReceiver = s.GetRequiredService<IMessageReceiver>();
				var handlerFactory = s.GetRequiredService<IHandlerFactory>();

				return new TransmittableProcessor(logger, messageReceiver, handlerFactory);
			});
		}
	}
}
