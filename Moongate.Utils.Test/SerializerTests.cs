using Moongate.Models.Transmittable;
using Moongate.State.Models;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Moongate.Utils.Tests
{
	public class SerializerTests
	{
		[Fact]
		public void Serialize ()
		{
			var serializer = new Serializer();
			var expected = new List<Transmission>
			{
				new Transmission
				{
					  SenderGuid = "abc"
				}
			};

			var serialized = serializer.Serialize(expected);

			List<Transmission> actual;
			using (var stream = new MemoryStream(serialized))
			{
				actual = ProtoBuf.Serializer.Deserialize<List<Transmission>>(stream);
			}

			Assert.Equal(expected.Count, actual.Count);
		}

		[Fact]
		public void Deserialize ()
		{
			var serializer = new Serializer();
			var expected = new Vector
			{
				x = 2,
				y = 3
			};

			byte[] serialized;
			using (var stream = new MemoryStream())
			{
				ProtoBuf.Serializer.Serialize(stream, expected);
				serialized = stream.ToArray();
			}
			var deserialized = serializer.Deserialize<Vector>(serialized);

			Assert.Equal(expected.x, deserialized.x);
		}
	}
}