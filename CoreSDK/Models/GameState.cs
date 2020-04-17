using CoreSDK.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using CoreSDK.Utils;

namespace CoreSDK
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
