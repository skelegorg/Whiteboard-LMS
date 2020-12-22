using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhiteboardAPI.Resources {
	public static class ErrorLogger {
		static string filePath = @"Resources\errorLog.txt";
		public async static void logError (Exception e) {
			if(File.Exists(filePath)) {
				DateTime localDateTime = DateTime.Now;
				// The format name is stupidly long
				string errorContents = localDateTime.ToString(System.Globalization.CultureInfo.InvariantCulture) + " " + e.Message;
				await File.AppendAllTextAsync(filePath, errorContents);
			} else {
				File.Create(filePath);
				logError(e);
			}
			
		}
	}
}

