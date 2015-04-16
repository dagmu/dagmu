using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace DagMUWPF.Windows
{
	public partial class Worlds : Window
	{
		public Worlds()
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
