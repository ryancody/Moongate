using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System;

namespace Moongate.Transmittable.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (TransmissionType messageType, IEventArgs payload);
	}
}
