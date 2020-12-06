using Moongate.Identity.Provider;
using Moongate.Logger;
using Moongate.Messaging.Handler;
using Moongate.Messaging.Messenger;
using Moongate.Transmittable.Factory;

namespace Moongate.IO
{
	public class Farspeaker
	{
		public Input Input { get; }
		public Output Output { get; }

		public Farspeaker (ILogger logger, 
							ITransmittableFactory transmittableFactory,
							IMessenger messenger, 
							IIdentityProvider identityProvider, 
							IHandlerProvider handlerProvider)
		{
			Input = new Input(logger, transmittableFactory, messenger, identityProvider);
			Output = new Output(logger, handlerProvider, identityProvider);
		}
	}
}
