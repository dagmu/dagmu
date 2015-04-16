using System;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
{
	public partial class ConsoleForm : HelperForm
	{
		public ConsoleForm()
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
