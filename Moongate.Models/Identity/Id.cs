using System;
using System.Collections.Generic;

namespace Moongate.Models.Identity
{
	public class Id
	{
		private static readonly List<string> NameList = new List<string>() { "Jon", "Sansa", "Robert", "Ned", "Geoff", "Breanne", "Jaime", "Cersei" };

		public string Guid = System.Guid.NewGuid().ToString();
		public string Name = NameList[new Random().Next(NameList.Count)];
		public string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public int ConnectionId = 0;
	}
}
