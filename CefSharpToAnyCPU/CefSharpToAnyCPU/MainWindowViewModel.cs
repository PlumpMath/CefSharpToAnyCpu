using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CefSharpToAnyCPU
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private string _url = "http://tvpot.daum.net/mypot/View.do?clipid=74918957&ownerid=mRlSExWR4-Q0";
		public string Url
		{
			get { return this._url; }
			set
			{
				this._url = value;
				this.OnPropertyChanged("Url");
			}
		}

		#region INotifyPropertyChanged implement

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		#endregion
	}
}
