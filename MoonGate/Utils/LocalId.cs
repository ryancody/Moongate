using System;
using System.Collections.Generic;

namespace Network
{
	public static class LocalId
	{
		static readonly List<string> NameList = new List<string>() { "Jon", "Sansa", "Robert", "Ned", "Geoff", "Breanne", "Jaime", "Cersei" };
		
		public static string Guid = System.Guid.NewGuid().ToString();
		public static string Name = NameList[new Random().Next(NameList.Count)];
		public static string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		public static int ConnectionId = 0;
	}
}
