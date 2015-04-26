using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
{
	public partial class InputBox : System.Windows.Forms.UserControl
	{
		const int bufferSize = 500;

		int lines; // height of this textbox in lines of text
		public int Lines
		{
			get { return lines; }
			set {
				lines = Math.Max(Math.Min(value,6),1);

				setlines();
			}
		}

		List<String> history;
		int historyCurrent;  // this is the last thing the user had in the inputbox

		// Constructors
		public InputBox()
		{
			InputBoxInit(InputBoxBox.defaultheight);
		}
		
		public InputBox(int numberoflines)
		{
			InputBoxInit(numberoflines);
		}

		public InputBox(IContainer container)
		{
			container.Add(this);
			InputBoxInit(InputBoxBox.defaultheight);
		}

		void InputBoxInit(int numberoflines)
		{
			InitializeComponent();
			InputBoxSetBindings();

			history = new List<String>();

			Lines = numberoflines;
		}

		void InputBoxSetBindings()
		{
			this.input.BackColor = global::DagMU.Forms.Properties.Settings.Default.InputBackColor;
			this.input.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::DagMU.Forms.Properties.Settings.Default, "InputBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			this.input.ForeColor = global::DagMU.Forms.Properties.Settings.Default.InputForeColor;
			this.input.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::DagMU.Forms.Properties.Settings.Default, "InputForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			this.input.Font = global::DagMU.Forms.Properties.Settings.Default.Font;
			this.input.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DagMU.Forms.Properties.Settings.Default, "Font", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
		}

		public event EventHandler<string> ESend;//send text to muck
		public event EventHandler ENew; // make new inputbox
		public event EventHandler EClose; // close this inputbox
		public event EventHandler<MouseEventArgs> EScroll;//pass scroll messages to main box

		public enum Status
		{
			Disconnected,	// disconnected, same as sending
			Sending,		// sending, disable
			Sent,			// sent, clear input
			Normal			// enable, refocus
		}

		public void refocus()
		{
			input.Focus();
		}

		public void newstatus(Status msg)
		{
			if (input.InvokeRequired) { this.Invoke((Action)(() => newstatus(msg) )); return; }

			switch (msg)
			{
				case Status.Disconnected:	// disconnected
				case Status.Sending:		// sending, grey textbox
					input.Enabled = false;
					break;
				case Status.Sent:			// line sent, ungrey and clear textbox
					input.Clear();
					goto case Status.Normal;
				case Status.Normal:
					input.Enabled = true;
					refocus();
					break;
			}
		}

		#region button stuff
		void setlines()
		{
			SuspendLayout();

			int height = input.Font.Height;
			
			this.Height = height * lines + input.Margin.Top + input.Margin.Bottom + Margin.Top + Margin.Bottom;	// This should fire the Resize event

			// now resolve button placement

			buttonClose.Height = buttonSmaller.Height = buttonBigger.Height = buttonMore.Height = height;
			buttonClose.Width = buttonMore.Width = splitContainer1.Panel2.Width;
			buttonSmaller.Width = buttonBigger.Width = splitContainer1.Panel2.Width / 2;
			buttonBigger.Left = buttonSmaller.Width;

			if (lines == 1)
			{
				buttonClose.Height = height / 2;

				buttonSmaller.Visible = false;

				buttonMore.Left = buttonClose.Left;
				buttonMore.Top = buttonClose.Height;
				buttonMore.Height = height - buttonClose.Height;
				buttonMore.Width = splitContainer1.Panel2.ClientRectangle.Width / 2;

				buttonBigger.Left = buttonMore.Width;
				buttonBigger.Top = buttonMore.Top;
				buttonBigger.Height = buttonMore.Height;
				buttonBigger.Width = splitContainer1.Panel2.ClientRectangle.Width - buttonMore.Width;

				ResumeLayout();
				return;
			}

			if (lines > 1)
				buttonSmaller.Visible = true;

			if (lines == 2)
			{
				buttonSmaller.Top = buttonBigger.Top = buttonClose.Height = buttonMore.Height = buttonBigger.Height = buttonSmaller.Height = height * 2 / 3;

				buttonMore.Top = splitContainer1.Panel2.Height - buttonMore.Height;

				ResumeLayout();
				return;
			}

			if (lines >= 3)
			{
				buttonSmaller.Top = buttonBigger.Top = height;

				buttonMore.Top = splitContainer1.Panel2.Height - buttonMore.Height;

				ResumeLayout();
				return;
			}
		}

		void buttonClose_Click(object sender, EventArgs e)
		{
			EClose(this, e);
		}

		void buttonSmaller_Click(object sender, EventArgs e)
		{
			Lines = Lines - 1;
			refocus();
		}

		void buttonBigger_Click(object sender, EventArgs e)
		{
			Lines = Lines + 1;
			refocus();
		}

		void buttonMore_Click(object sender, EventArgs e)
		{
			ENew(sender, e);
		}
#endregion

		void input_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EScroll(sender, e);//pass mousewheel events to the main text window
		}

		void input_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
		{
			switch (e.KeyCode) {
				case Keys.PageUp:
					EScroll(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, 0, 0, 300));
					break;

				case Keys.PageDown:
					EScroll(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, 0, 0, -300));
					break;
			}
		}

		void input_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch (e.KeyCode) {
				case Keys.Up:
					if (history.Count == 0) break;
					if (historyCurrent > 0) historyCurrent--;
					else break;//reached end of history
					input.Text = history[historyCurrent];
					input.SelectionStart = input.TextLength;
					break;

				case Keys.Down:
					if (history.Count == 0) return;
					if (historyCurrent < history.Count) historyCurrent++;
					if (historyCurrent != history.Count) {
						input.Text = history[historyCurrent];
					} else {
						input.Text = String.Empty;
					}
					input.SelectionStart = input.TextLength;
					break;

				/*case Keys.Oemplus:
					if (input.Text == "p " || input.Text == "page ") {
						input.Text = "p " + lastPagedName + "=";//insert last paged name
						input.SelectionStart = input.TextLength;
						e.SuppressKeyPress = true;
					}
					else if (input.Text == "w " || input.Text == "wh ") {
						input.Text = "p " + lastWhisperedName + "=";//insert last paged name
						input.SelectionStart = input.TextLength;
						e.SuppressKeyPress = true;
					}
					break;*/

				case Keys.Enter:
					if (e.Control) break;
					string trimmed = input.Text.Trim();
					if (trimmed.Length == 0) { e.SuppressKeyPress = true; break; }
					ESend(this, trimmed);
					if (history.Count == 0) { history.Add(trimmed); historyCurrent = 1; }
					else if (trimmed != history[history.Count - 1]) { history.Add(trimmed); historyCurrent++; }
					e.SuppressKeyPress = true;
					break;
			}
		}

		void input_TextChanged(object sender, EventArgs e)
		{
			if (input.TextLength > bufferSize) {
				int carat = input.SelectionStart;
				input.Select(0, bufferSize);
				input.SelectionColor = input.ForeColor;
				input.Select(bufferSize, input.TextLength);
				input.SelectionColor = Color.Red;
				input.SelectionStart = carat;
			}
		}
	}
}
