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
using DagMU.Model;

namespace DagMUWPF.Windows
{
	/// <summary>
	/// Interaction logic for winConsole.xaml
	/// </summary>
	public partial class Console : Helper, IConsole
	{
		public Console()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Add text to the console window
		/// </summary>
		public void Print(String s)
		{
			textBox.AppendText(s);
			textBox.ScrollToEnd();
		}
	}
}
