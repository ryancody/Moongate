using Network.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Network.Utils;

namespace Network
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
