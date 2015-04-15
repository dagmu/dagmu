using System;
using System.Windows.Forms;

namespace DagMU.Forms.HelperWindows
{
	public partial class HelperForm : Form, IHelper
	{
		public HelperForm()
		{
			InitializeComponent();
		}

		public World parent;

		void HelperForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			Hide();
			e.Cancel = true;
		}

		void checkBoxOnTop_CheckedChanged(object sender, EventArgs e)
		{
			this.TopMost = checkBoxOnTop.Checked;
		}
	}
}
