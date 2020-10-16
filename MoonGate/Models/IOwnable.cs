using System;
using System.Collections.Generic;
using System.Text;

namespace Network.Models
{
	public interface IOwnable
	{
		string Owner { get; set; }
	}
}
