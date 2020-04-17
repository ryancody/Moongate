using CoreSDK.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Models
{
	[Serializable]
	public class Transmittable : ITransmittable
	{
		public static Transmission Deserialized (byte[] t)
		{
			var deser = Serializer.ByteArrayToObject<Transmission>(t);
			return deser;
		}

		public byte[] Serialized ()
		{
			return Serializer.ObjectToByteArray(this);
		}
	}
}
