using System;

namespace Moongate.State.Models
{
	[Serializable]
	public class Vector : IEquatable<object>
	{
		public float x { get; set; }
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
