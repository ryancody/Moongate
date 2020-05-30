using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Models
{
	[Serializable]
	public class Vector
	{
		public float x { get; set; }
		public float y { get; set; }
		public float Magnitude { get; set; }
	}
}
