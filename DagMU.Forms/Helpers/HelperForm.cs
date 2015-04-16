using System;
using System.Windows.Forms;
using DagMU.Model;

namespace DagMU.Forms.Helpers
{
	public partial class HelperForm : Form, IHelper
	{
		public HelperForm()
		{
			InitializeComponent();
		}

		internal WorldVM parent;
		internal int index;

		void SetParent(WorldVM parent)
		{
			this.parent = parent;
		}

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
