using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Moongate.Logger
{
	public enum LoggerLevel
	{
		Error,
		Info,
		Warning,
		Debug
	}

	public class Logger : ILogger
	{
		private readonly string prefix = "";
		private readonly string fileName;
		private readonly string guid;
		private IEnumerable<string> writeBuffer;
		private readonly LoggerLevel[] logFilter;


		public Logger (string prefix, LoggerLevel[] logFilter, string guid = "")
		{
			
			this.prefix = prefix;
			this.logFilter = logFilter;
			this.guid = guid;
			fileName = BuildFileName();
			writeBuffer = new List<string>();

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

		public void Warning (string message)
		{
			Write(message, LoggerLevel.Warning);
		}

		void Write (string message, LoggerLevel level)
		{
			if (!logFilter.Contains(level))
			{
				return;
			}
			
			// Set a variable to the Documents path.
			string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
			string filePath = Path.Combine(docPath, "RpgLogs");

			if (!Directory.Exists(filePath))
			{
				Directory.CreateDirectory(filePath);
			}

			File.AppendAllText(Path.Combine(filePath, fileName), DateTime.Now + " - " + prefix + " " + level.ToString() + " " + " - " + message + "\n");
		}
	}
}
