using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml;

namespace DagMUWPF.Windows
{
	public partial class Worlds : Helper
	{
		public Worlds()
		{
			InitializeComponent();
			XmlDataProvider xdp = this.TryFindResource("worldsXML") as XmlDataProvider;
            if (xdp != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load("dagmu.xml");
                xdp.Document = doc;
                xdp.XPath = "/DagMU/Worlds/World";
            }
			//WorldsXML.Source = new Uri(Utils.AssemblyDirectory + Path.DirectorySeparatorChar + WorldsXML.Source);
		}
		
		private void Save()
		{
			//WorldsXML.Document.Save(WorldsXML.Source.LocalPath);
		}
		
		private void TextBox_LostFocus(object sender, RoutedEventArgs e)
		{
			this.Save();
		}
	}
}
