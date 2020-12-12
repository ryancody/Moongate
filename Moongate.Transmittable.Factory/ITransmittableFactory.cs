using Moongate.Models.Transmittable;
using System;

namespace Moongate.Transmittable.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (TransmissionType messageType, EventArgs payload);
	}
}
