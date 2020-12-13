using System;

namespace Moongate.State.Models
{
	[Serializable]
	public class Vector : IEquatable<Vector>
	{
		public float x { get; set; }
		public float y { get; set; }
		public float Magnitude { get; set; }

		public bool Equals (Vector other) =>
			x == other.x
			&& y == other.y
			&& Magnitude == other.Magnitude;
	}
}
