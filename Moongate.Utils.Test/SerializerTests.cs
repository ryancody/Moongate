using GroBuf;
using GroBuf.DataMembersExtracters;
using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System.Collections.Generic;
using Xunit;

namespace Moongate.Utils.Tests
{
	public class SerializerTests
	{
		private readonly GroBuf.Serializer serializer;

		public SerializerTests ()
		{
			serializer = new GroBuf.Serializer(new AllFieldsExtractor(), options: GroBufOptions.WriteEmptyObjects);
		}

		[Fact]
		public void Serialize ()
		{
			var expected = new Queue<Transmission>();
			expected.Enqueue(
				new Transmission
				{
					Payload = new PingArgs
					{
						 InitialTimestamp = 1,
						  InitiatorGuid = "a",
						   Ping = 2
					},
					SenderGuid = "b",
					TransmissionType = TransmissionType.PlayerConnected
				});

			var serialized = serializer.Serialize(expected);

			var actual = serializer.Deserialize<Queue<Transmission>>(serialized);

			if (actual.Peek().Payload is PingArgs p)
			{
				var t = p;
			}

			Assert.Equal(expected.Count, actual.Count);
		}
	}
}