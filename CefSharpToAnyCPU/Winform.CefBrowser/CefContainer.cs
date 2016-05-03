using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using Winform.CefBrowser.Utils;

namespace Winform.CefBrowser
{
	public class CefContainer
	{
		private static bool IsInitialized;

		// Use when debugging the actual SubProcess, to make breakpoints etc. inside that project work.
		private const bool debuggingSubProcess = false;

		public static void Init()
		{
			if (IsInitialized)
			{
				return;
			}

			//var core = Assembly.LoadFrom(@"CefSharp.Core.dll");

			var settings = new CefSettings();
			settings.RemoteDebuggingPort = 8088;
			settings.LogSeverity = LogSeverity.Verbose;
			settings.CachePath = "cache";
			if (debuggingSubProcess)
			{
				settings.BrowserSubprocessPath = "..\\..\\..\\..\\CefSharp.BrowserSubprocess\\bin\\x86\\Debug\\CefSharp.BrowserSubprocess.exe";
			}

			//settings.CefCommandLineArgs.Add("disable-plugins-discovery", "1"); //Disable discovering third-party plugins. Effectively loading only ones shipped with the browser plus third-party ones as specified by --extra-plugin-dir and --load-plugin switches
			//settings.CefCommandLineArgs.Add("enable-system-flash", "1"); //Automatically discovered and load a sy
			//settings.CefCommandLineArgs.Add("enable-npapi", "1"); //Enable NPAPI plugs which were disabled by default in Chromium 43 (NPAPI will be removed completely in Chromium 45)
			//settings.CefCommandLineArgs.Add("ppapi-flash-path", @"C:\Program Files (x86)\Google\Chrome\Application\49.0.2623.87\PepperFlash\pepflashplayer.dll"); //Load a specific pepper flash version (Step 1 of 2)
			//settings.CefCommandLineArgs.Add("ppapi-flash-version", "20.0.0.306"); //Load a specific pepper flash version (Step 2 of 2)
			var flashPath = FlashPathFinder.GetFlashPath();
			var version = FlashPathFinder.GetFlashVersion(flashPath);
			settings.CefCommandLineArgs.Add("ppapi-flash-path", flashPath); //Load a specific pepper flash version (Step 1 of 2)
			settings.CefCommandLineArgs.Add("ppapi-flash-version", version); //Load a specific pepper flash version (Step 2 of 2)

			//settings.RegisterScheme(new CefCustomScheme
			//{
			//	SchemeName = CefSharpSchemeHandlerFactory.SchemeName,
			//	SchemeHandlerFactory = new CefSharpSchemeHandlerFactory()
			//});

			if (!Cef.Initialize(settings))
			{
				if (Environment.GetCommandLineArgs().Contains("--type=renderer"))
				{
					Environment.Exit(0);
				}
				else
				{
					return;
				}
			}

			IsInitialized = true;
		}

		public static void ShutDown()
		{
			try
			{
				Cef.Shutdown();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
		}
	}
}
