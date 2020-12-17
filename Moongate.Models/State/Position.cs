using System;

namespace Moongate.State.Models
{
	[Serializable]
	public class Position
	{
		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; }

		public Position (float x, float y, float z) {
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public Position (float x, float y)
		{
			this.x = x;
			this.y = y;
			z = 0;
		}

		public Position () { }

		public override string ToString ()
		{
			return x + ", " + y + ", " + z;
		}
	}
}
