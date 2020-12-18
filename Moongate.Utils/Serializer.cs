using System.IO;

namespace Moongate.Utils
{
	public class Serializer : ISerializer
	{
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
				using (var stream = new MemoryStream())
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
