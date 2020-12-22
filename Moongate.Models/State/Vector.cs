using ProtoBuf;
using System;

namespace Moongate.State.Models
{
	[ProtoContract]
	public class Vector : IEquatable<object>
	{
		[ProtoMember(1)]
		public float x { get; set; }
		[ProtoMember(2)]
		public float y { get; set; }

		public override bool Equals (object obj) =>
			obj != null
			&& obj is Vector other
			&& x.Equals(other.x)
			&& y.Equals(other.y);

		public override int GetHashCode () => base.GetHashCode();

		public static Vector Zero = new Vector();
	}
}
