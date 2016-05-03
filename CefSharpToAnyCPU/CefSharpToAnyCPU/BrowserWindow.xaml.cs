using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Winform.CefBrowser;

namespace CefSharpToAnyCPU
{
	/// <summary>
	/// Interaction logic for BrowserWindow.xaml
	/// </summary>
	public partial class BrowserWindow : Window
	{
		private CefWebBrowser _browser;
		private string _initUrl;

		public BrowserWindow(string url)
		{
			InitializeComponent();
			this._initUrl = url;
			this.Loaded += BrowserWindow_Loaded;
		}

		void BrowserWindow_Loaded(object sender, RoutedEventArgs e)
		{
			_browser = new CefWebBrowser(this._initUrl);
			this._browser.AutoSize = true;
			this.xWinFormHost.Child = this._browser;

			this.Unloaded += BrowserWindow_Unloaded;
		}

		void BrowserWindow_Unloaded(object sender, RoutedEventArgs e)
		{
			this.xWinFormHost.Dispose();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (this._browser == null)
			{
				return;
			}

			this._browser.LoadUrl(this.xUrlString.Text);
		}
	}
}
