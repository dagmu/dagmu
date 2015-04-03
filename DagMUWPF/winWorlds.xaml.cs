using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DagMUWPF
{
	public partial class winWorlds : Window
	{
		public winWorlds()
		{
			InitializeComponent();
			WorldsData.Source = new Uri(Utils.AssemblyDirectory + Path.DirectorySeparatorChar + WorldsData.Source);
		}
		
		private void Save()
		{
			WorldsData.Document.Save(WorldsData.Source.LocalPath);
		}
		
		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			this.Save();
		}
	}
}
