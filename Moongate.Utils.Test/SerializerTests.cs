using Moongate.Models.Events;
using Moongate.Models.Transmittable;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Moongate.Utils.Tests
{
	public class SerializerTests
	{
		private readonly Serializer serializer;
		public SerializerTests ()
		{
			serializer = new Serializer();
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

			if (actual.ToList().First().Payload is PingArgs p
				&& expected.ToList().First().Payload is PingArgs pExpected)
			{
				Assert.Equal(pExpected.InitialTimestamp, p.InitialTimestamp);
			}

			Assert.Equal(expected.Count, actual.Count);
			Assert.Equal(expected.ToList().First().SenderGuid, actual.ToList().First().SenderGuid);
			Assert.Equal(expected.ToList().First().TransmissionType, actual.ToList().First().TransmissionType);
			Assert.Equal(expected.ToList().First().Payload.GetType(), actual.ToList().First().Payload.GetType());
		}
	}
}