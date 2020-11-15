using Moongate.Identity.Provider;
using Moongate.Logger;
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
		private readonly ILogger logger;
		private readonly IHandlerProvider handlerFactory;
		private readonly IIdentityProvider identityProvider;

		public TransmittableProcessor (ILogger logger, 
										IMessageReceiver messageReceiver, 
										IHandlerProvider handlerFactory, 
										IIdentityProvider identityProvider)
		{
			this.logger = logger;
			this.handlerFactory = handlerFactory;
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
					var handler = handlerFactory.GetHandler(message.TransmissionType);

					handler.Handle(message);

					logger.Debug($@"Message handled:
						- {message.SenderGuid}
						- {message.TransmissionType}");
				}
			}
			catch (Exception e)
			{
				logger.Error(e.ToString());
				Console.WriteLine(e);
			}
		}
	}
}
