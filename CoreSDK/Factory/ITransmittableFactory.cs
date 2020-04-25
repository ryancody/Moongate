using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Factory
{
	public interface ITransmittableFactory
	{
		ITransmittable Build (MessageType messageType, object payload);
	}
}
