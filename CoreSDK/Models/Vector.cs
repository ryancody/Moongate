using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Models
{
	[Serializable]
	public class Vector
	{
		public float? x { get; set; }
		public float? y { get; set; }
		public float? Magnitude { get; set; }

		public override bool Equals (object obj)
		{
			if (obj == null)
			{
				return false;
			}

			Vector compare;

			try
			{
				compare = (Vector)obj;
			}
			catch
			{
				throw new Exception("Failed to cast object to type Vector");
			}

			if (x.GetValueOrDefault(0) == compare.x.GetValueOrDefault(0)
			&& y.GetValueOrDefault(0) == compare.y.GetValueOrDefault(0)
			&& Magnitude.GetValueOrDefault(0) == compare.Magnitude.GetValueOrDefault(0))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
