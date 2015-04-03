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

		public event World.StringDelegate ESend; // send text to muck
		public event World.NullDelegate EStatusReset; // reset status to normal

		void textbox_KeyDown(object sender, KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.F5 || (e.Control && e.KeyCode == Keys.Enter) ) {
				if (String.IsNullOrEmpty(textbox.Text)) {
					e.SuppressKeyPress = true;
					return;
				}

				if (textbox.SelectionLength > 0)
					foreach (String line in textbox.SelectedText.Split(new[]{'\n','\r'}, StringSplitOptions.RemoveEmptyEntries))
						ESend(line);
				else
					foreach (String line in textbox.Lines) ESend(line);

				e.SuppressKeyPress = true;
			}
		}

		void buttonResetNormal_Click(object sender, EventArgs e)
		{
			EStatusReset();
		}

		public void UpdateStatus(String newstatus)
		{
			if (InvokeRequired) {
				World.StringDelegate callback = new World.StringDelegate(UpdateStatus);
				this.Invoke(callback, newstatus);
			}

			labelStatus.Text = newstatus;
		}

		void textboxCharName_TextChanged(object sender, EventArgs e)
		{
			parent.CharName = textboxCharName.Text;
		}
	}
}
