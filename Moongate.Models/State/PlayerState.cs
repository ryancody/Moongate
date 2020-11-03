using System.Collections.Generic;

namespace Moongate.State.Models
{
	public class PlayerState
	{
		/// <summary>
		/// Guid to Player pair
		/// </summary>
		public Dictionary<string, Player> Players { get; set; } = new Dictionary<string, Player>();

		/// <summary>
		/// ConnectionId to Guid pair
		/// </summary>
		public Dictionary<int, string> GUIDs { get; set; } = new Dictionary<int, string>();
	}
}
