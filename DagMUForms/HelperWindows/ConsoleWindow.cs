using System;
using System.Windows.Forms;

namespace DagMU.HelperWindows
{
	public partial class ConsoleWindow : HelperWindow
	{
		public ConsoleWindow()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Add text to the console window
		/// </summary>
		public void Print(String s)
		{
			if (textBox1.InvokeRequired) { this.Invoke((Action)(() => Print(s) )); return; }

            textBox1.AppendText(s);
            textBox1.ScrollToCaret();
		}
	}
}
