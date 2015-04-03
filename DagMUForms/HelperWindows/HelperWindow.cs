using System;
using System.Windows.Forms;

namespace DagMU.HelperWindows
{
	public partial class HelperWindow : Form
	{
		public HelperWindow()
		{
			InitializeComponent();
		}

		public World parent;

		void HelperWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			Hide();
			e.Cancel = true;
		}

		void checkBoxOnTop_CheckedChanged(object sender, EventArgs e)
		{
			TopMost = checkBoxOnTop.Checked;
		}
	}
}
