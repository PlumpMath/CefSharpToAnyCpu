using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.Diagnostics;

namespace Winform.CefBrowser
{
	public partial class CefWebBrowser : UserControl
	{
		public IWinFormsWebBrowser Browser { get; private set; }

		private ChromiumWebBrowser _browser;

		static CefWebBrowser()
		{
			CefContainer.Init();
		}

		public CefWebBrowser(string url)
		{
			InitializeComponent();
			
			//Init();

			this._browser = new ChromiumWebBrowser(url)
			{
				Dock = DockStyle.Fill
			};

			this.Controls.Add(_browser);

			Browser = _browser;

			//Browser.MenuHandler = new MenuHandler();
			//Browser.NavStateChanged += OnBrowserNavStateChanged;
			//Browser.ConsoleMessage += OnBrowserConsoleMessage;
			//Browser.TitleChanged += OnBrowserTitleChanged;
			//Browser.AddressChanged += OnBrowserAddressChanged;
			//Browser.StatusMessage += OnBrowserStatusMessage;

			//var version = String.Format("Chromium: {0}, CEF: {1}, CefSharp: {2}", Cef.ChromiumVersion, Cef.CefVersion, Cef.CefSharpVersion);
			//DisplayOutput(version);

			Disposed += BrowserTabUserControlDisposed;
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (this._browser != null)
			{
				this._browser.Width = this.Width;
				this._browser.Height = this.Height;
			}
		} 

		private void BrowserTabUserControlDisposed(object sender, EventArgs e)
		{
			Disposed -= BrowserTabUserControlDisposed;

			//Browser.NavStateChanged -= OnBrowserNavStateChanged;
			//Browser.ConsoleMessage -= OnBrowserConsoleMessage;
			//Browser.TitleChanged -= OnBrowserTitleChanged;
			//Browser.AddressChanged -= OnBrowserAddressChanged;
			//Browser.StatusMessage -= OnBrowserStatusMessage;

			Browser.Dispose();
		}

		public void LoadUrl(string url)
		{
			if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
			{
				Browser.Load(url);
			}
		}
	}
}
