using System;
using System.Collections.Generic;

namespace Moongate.State.Models
{
	[Serializable]
	public class GameState
	{
		public Dictionary<string, Entity> Entities { get; set; }

		public GameState () 
		{
			Entities = new Dictionary<string, Entity>();
		}
	}
}
