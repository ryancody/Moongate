using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK.Models
{
	public interface IOwnable
	{
		string Owner { get; set; }
	}
}
