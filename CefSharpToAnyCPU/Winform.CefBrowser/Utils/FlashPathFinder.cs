using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Winform.CefBrowser.Utils
{
	public class FlashPathFinder
	{
		public static string GetFlashPath()
		{
			var applicationPath = @"C:\Program Files (x86)\Google\Chrome\Application\";
			var folders = from path in Directory.GetDirectories(applicationPath)
						  let folderName = path.Replace(applicationPath, string.Empty)
						  where IsVersionType(folderName)
						  orderby folderName descending
						  select folderName;

			var upToDatePath = folders.FirstOrDefault();
			if (string.IsNullOrWhiteSpace(upToDatePath))
			{
				return null;
			}

			return string.Format(@"{0}{1}\PepperFlash\pepflashplayer.dll", applicationPath, upToDatePath);
		}

		private static bool IsVersionType(string target)
		{
			// e.g 49.0.2623.87 is true;
			var versionParts = target.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries);
			if (versionParts.Length != 4)
			{
				return false;
			}

			return versionParts.All(part => part.All(p => char.IsNumber(p)));	
		}

		public static string GetFlashVersion(string path)
		{
			//var path = @"C:\Program Files (x86)\Google\Chrome\Application\49.0.2623.87\PepperFlash\pepflashplayer.dll";
			var folderParts = path.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).ToList();
			folderParts.RemoveAt(folderParts.Count - 1);
			var dir = string.Join("\\", folderParts) + "\\manifest.json";
			if (File.Exists(dir) == false)
			{
				return string.Empty;
			}

			var version = string.Empty;
			using(var reader = new StreamReader(dir))
			{
				while (true)
				{
					var line = reader.ReadLine();
					if (line == null)
					{
						break;
					}

					if (line.Trim().StartsWith("version") == false)
					{
						continue;
					}

					var lineParts = line.Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
					if (lineParts.Length != 2)
					{
						return string.Empty;
					}

					version = lineParts[1];
					break;
				}
			}
			
			return version.Replace("\"", string.Empty);
		}
	}
}
