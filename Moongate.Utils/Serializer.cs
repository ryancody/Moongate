using GroBuf;
using GroBuf.DataMembersExtracters;

namespace Moongate.Utils
{
	public class Serializer : ISerializer
	{
		GroBuf.Serializer grobufSerializer;

		public Serializer ()
		{
			grobufSerializer = new GroBuf.Serializer(new AllFieldsExtractor(), options: GroBufOptions.WriteEmptyObjects);
		}

		public byte[] Serialize<T> (T obj)
		{
			try
			{
				return grobufSerializer.Serialize<T>(obj);
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
				return grobufSerializer.Deserialize<T>(bytes);
			}
			catch
			{
				throw;
			}
		}
	}
}
