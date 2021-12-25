using System;
using System.Collections.Generic;

namespace Moongate.Models.Identity
{
	public class Id
	{
		private static readonly List<string> NameList = new List<string>() { "Jon", "Sansa", "Robert", "Ned", "Joff", "Breanne", "Jaime", "Cersei", "Theon", "Arya", "Tyrion", "Petyr", "Stannis" };

		public string Guid = System.Guid.NewGuid().ToString();
		public string Name = NameList[new Random().Next(NameList.Count)];
		public string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public int ConnectionId = 0;
		public bool IsServer { get; set; }
	}
}
