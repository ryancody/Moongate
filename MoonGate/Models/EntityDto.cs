using System;

namespace Network.Models
{
	[Serializable]
	public class EntityDto : IOwnable
	{
		public string Owner { get; set; }
		public string Guid { get; set; }
		public string Id { get; set; }
		public string Name { get; set; }
		public Position Position { get; set; }
	}
}