using System;
using System.Collections.Generic;
using System.IO;

namespace Moongate.Logger
{
	public enum LoggerLevel
	{
		Error,
		Info,
		Debug
	}

	public class Logger : ILogger
	{
		private readonly string prefix = "";
		private readonly string fileName;
		private readonly string filePath;
		private readonly string guid;
		private readonly LoggerLevel logLevel;


		public Logger (string prefix, LoggerLevel logLevel, string guid = "")
		{
			this.prefix = prefix;
			this.logLevel = logLevel;
			this.guid = guid;
			fileName = BuildFileName();

			string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			filePath = Path.Combine(docPath, "RpgLogs");

			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}
		}

		string BuildFileName ()
		{
			return $"{prefix}-{DateTime.Now.ToString($"MM-dd-yyyy-hh-mm-sstt")}-{guid.Substring(0, 4)}.txt";
		}
		
		public void Error (string message)
		{
			Write(message, LoggerLevel.Error);
		}

		public void Info (string message)
		{
			Write(message, LoggerLevel.Info);
		}

		public void Debug (string message)
		{
			Write(message, LoggerLevel.Debug);
		}

		void Write (string message, LoggerLevel level)
		{
			if (level <= logLevel)
			{
				File.AppendAllText(Path.Combine(filePath, fileName), DateTime.Now + " - " + prefix + " " + level.ToString() + " " + " - " + message + "\n");
			}
		}
	}
}
