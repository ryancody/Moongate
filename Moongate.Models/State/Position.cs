using System;

namespace Moongate.State.Models
{
	[Serializable]
	public class Position
	{
		public float x { get; set; }
		public float y { get; set; }
		public float z { get; set; }

		public Position (float xVal, float yVal, float zVal) {
			x = xVal;
			y = yVal;
			z = zVal;
		}

		public Position (float xVal, float yVal)
		{
			x = xVal;
			y = yVal;
			z = 0;
		}

		public Position () { }

		public override string ToString ()
		{
			return x + ", " + y + ", " + z;
		}
	}
}
