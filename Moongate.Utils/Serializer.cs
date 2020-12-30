using Moongate.Models.Transmittable;
using System.IO;

namespace Moongate.Utils
{
	public class Serializer : ISerializer
	{
		public Serializer ()
		{
			ProtoBuf.Serializer.PrepareSerializer<ITransmittable>();
		}

		public void PrepareType<T> ()
		{
			ProtoBuf.Serializer.PrepareSerializer<T>();
		}

		public byte[] Serialize<T> (T obj)
		{
			try
			{
				using (var stream = new MemoryStream())
				{
					ProtoBuf.Serializer.Serialize<T>(stream, obj);
					return stream.ToArray();
				}
			}
			catch
			{
				throw;
			}
		}

		public T Deserialize<T> (byte[] bytes)
		{
			try
			{
				using (var stream = new MemoryStream(bytes))
				{
					return ProtoBuf.Serializer.Deserialize<T>(stream);
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
