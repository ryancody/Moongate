
using System;

namespace Network
{
	[Serializable]
	public class Player
	{
		public string Guid { get; set; }
		public int ConnectionId { get; set; }
		public string Name { get; set; }
	}
}
