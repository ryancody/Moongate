using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Utils
{
	public interface ISerializer
	{
		byte[] Serialize (object o);
		T Deserialize<T> (byte[] b);
	}
}
