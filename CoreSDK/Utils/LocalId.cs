using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSDK
{
	public static class LocalId
	{
		static List<string> NameList = new List<string>() { "Frank", "Tommy", "Rainie", "Jonny", "Valencia", "Fern", "Axel", "Conner", "Greta", "Hershel", "Bernadette", "Aberdeen" };
		
		public static string Guid = System.Guid.NewGuid().ToString();
		public static string Name = NameList[new Random().Next(NameList.Count)];
		public static string Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
	}
}
