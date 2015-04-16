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

		internal WorldVM parent { get; set; }
		internal int index { get; set; }

		void IHelper.SetIndex(int i)
		{
			index = i;
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
