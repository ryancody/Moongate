using System;

namespace Moongate.State.Models
{
	[Serializable]
	public class Entity : IOwnable
	{
		public string Owner { get; set; }
		public string Guid { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
	}
}