using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DagMU.HelperWindows
{
	public partial class DebugWindow : HelperWindow
	{
		public DebugWindow()
		{
			InitializeComponent();
		}

		public event EventHandler<string> ESend; // send text to muck
		public event EventHandler EStatusReset; // reset status to normal

		void textbox_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.F5 || (e.Control && e.KeyCode == Keys.Enter) ) {
				if (String.IsNullOrEmpty(textbox.Text)) {
					e.SuppressKeyPress = true;
					return;
				}

				if (textbox.SelectionLength > 0)
					foreach (String line in textbox.SelectedText.Split(new[]{'\n','\r'}, StringSplitOptions.RemoveEmptyEntries))
						ESend(null, line);
				else
					foreach (String line in textbox.Lines) ESend(null, line);

				e.SuppressKeyPress = true;
			}
		}

		void buttonResetNormal_Click(object sender, EventArgs e)
		{
			EStatusReset(null, null);
		}

		public void UpdateStatus(String newstatus)
		{
			if (InvokeRequired) { this.Invoke((Action)(() => UpdateStatus(newstatus))); return; }

			labelStatus.Text = newstatus;
		}

		void textboxCharName_TextChanged(object sender, EventArgs e)
		{
			parent.CharName = textboxCharName.Text;
		}
	}
}
