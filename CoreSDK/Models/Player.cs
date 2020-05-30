
using CoreNET.Controllers.Messenger;
using System;

namespace CoreSDK
{
	[Serializable]
	public class Player
	{
		public string GUID { get; set; }
		public ConnectionId ConnectionId { get; set; }
		public string Name { get; set; }
	}
}
