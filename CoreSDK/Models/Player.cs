
using System;

namespace CoreSDK
{
	[Serializable]
	public class Player
	{
		public string GUID { get; set; }
		public int ConnectionId { get; set; }
		public string Name { get; set; }
	}
}
