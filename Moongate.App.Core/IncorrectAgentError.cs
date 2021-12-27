using System;
using System.Collections.Generic;
using System.Text;

namespace Moongate.App
{
	public class IncorrectAgentError : Exception
	{

		public IncorrectAgentError (string message) : base(message)
		{ 
			
		}
	}
}
