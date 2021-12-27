using Microsoft.Extensions.Logging;
using Moongate.Identity.Provider;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Receiver;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Moongate.Transmittable.Processor.Test")]
namespace Moongate.Transmittable.Processor
{
	public class TransmittableProcessor : ITransmittableProcessor
	{
		private readonly ILogger<TransmittableProcessor> logger;
		private readonly IHandlerProvider handlerProvider;
		private readonly IIdentityProvider identityProvider;

		public TransmittableProcessor (ILogger<TransmittableProcessor> logger, 
										IMessageReceiver messageReceiver, 
										IHandlerProvider handlerProvider, 
										IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.handlerProvider = handlerProvider;
			this.identityProvider = identityProvider;

			messageReceiver.TransmissionReceived += OnTransmissionReceived;
		}

		internal void OnTransmissionReceived (object sender, TransmissionArgs a) 
		{
			Process(a.Transmission);
		}

		internal void Process (ITransmittable message)
		{
			try
			{
				if (message.SenderGuid != identityProvider.Id.Guid)
				{
					var handler = handlerProvider.GetHandler(message.TransmissionType);

					handler.Handle(message);

					logger.LogDebug($@"Message handled:
						- {message.SenderGuid}
						- {message.TransmissionType}");
				}
			}
			catch (Exception e)
			{
				logger.LogError(e.ToString());
				Console.WriteLine(e);
			}
		}
	}
}
