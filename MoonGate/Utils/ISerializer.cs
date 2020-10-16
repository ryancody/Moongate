using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Utils
{
	public interface ISerializer
	{
		byte[] Serialize (object o);
		T Deserialize<T> (byte[] b);
	}
}
