using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Model;

namespace DagMUWPF.Windows
{
	/// <summary>
	/// Interaction logic for Settings.xaml
	/// </summary>
	public partial class Settings : Helper
	{
		public Settings()
		{
			InitializeComponent();
			gridStuffToMatch.DataContext = stuffToMatch;
		}

		public ObservableCollection<Data.TextMatch> stuffToMatch {
			get {
				return stuffToMatchList
					?? new ObservableCollection<Data.TextMatch>() { new Data.TextMatch("page", new Data.ColorRGB(200, 0, 0)) };
			}
			set {
				stuffToMatchList = value;
				gridStuffToMatch.ItemsSource = value;
			}
		}
		private ObservableCollection<Data.TextMatch> stuffToMatchList;

		private ObservableCollection<Data.TextMatch> namesToMatch {
			get {
				return namesToMatchList
					?? new ObservableCollection<Data.TextMatch>() { new Data.TextMatch("Dagon", new Data.ColorRGB(200, 100, 0)) };
			}
			set {
				namesToMatchList = value;
			}
		}
		private ObservableCollection<Data.TextMatch> namesToMatchList;

		public void IoC(Tuple<ObservableCollection<Data.TextMatch>, ObservableCollection<Data.TextMatch>> ioc)
		{
			stuffToMatch = ioc.Item1;
			namesToMatch = ioc.Item2;
			//gridStuffToMatch.DataContext = this;
			//gridStuffToMatch.ItemsSource = stuffToMatch;
			//namesToMatch = ioc.Item2;
		}
	}
}
