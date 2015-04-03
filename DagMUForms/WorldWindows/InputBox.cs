using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DagMU.HelperWindows
{
	public partial class InputBox : System.Windows.Forms.UserControl
	{
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
		int historycurrent;  // this is the last thing the user had in the inputbox

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
			this.input.BackColor = global::DagMU.Properties.Settings.Default.InputBackColor;
			this.input.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::DagMU.Properties.Settings.Default, "InputBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			this.input.ForeColor = global::DagMU.Properties.Settings.Default.InputForeColor;
			this.input.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::DagMU.Properties.Settings.Default, "InputForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			this.input.Font = global::DagMU.Properties.Settings.Default.Font;
			this.input.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DagMU.Properties.Settings.Default, "Font", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
		}

		public delegate void TextMessage(InputBox sender, string msg);
		public delegate void RefMessage(InputBox sender);
		public delegate void ScrollMessage(MouseEventArgs mouseeventargs);

		public event TextMessage ESend; // send text to muck
		public event RefMessage ENew; // make new inputbox
		public event RefMessage EClose; // close this inputbox
		public event ScrollMessage EScroll; // pass mousewheel messages to main window

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

		public delegate void InputMsgDelegate(Status msg);
		public void newstatus(Status msg)
		{
			// if we're in the wrong thread, pass the message to the right thread
			if (input.InvokeRequired)
			{
				InputMsgDelegate callback = new InputMsgDelegate(newstatus);
				this.Invoke(callback, msg);
				return;
			}

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
			EClose(this);
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
			ENew(null);
		}
#endregion

		void input_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// pass mousewheel events to the main text window
			EScroll(e);
		}

		void input_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// TODO inputbox code to convert
/*
	//if (e->keycode == Keys:

	// F2 debug test key hack
	//if (e->KeyCode == Keys::F2) {
	//	for each( String^ word in history )
	//		input->Text += word + " ";
	//	return;
	//}

	// PAGEUP/PAGEDOWN to scroll main window
	if (e->KeyCode == Keys::PageUp)
	{
		SendScrollEvent((RichTextBox^)parent->box, SB_PAGEUP);
		e->SuppressKeyPress = true;
		return;
	}
	if (e->KeyCode == Keys::PageDown)
	{
		SendScrollEvent((RichTextBox^)parent->box, SB_PAGEDOWN);
		e->SuppressKeyPress = true;
		return;
	}
	//PAGEUP/PAGEDOWN

	// TAB
	// Note: I was going to make the tab key put a few spaces into the input box,
	// but spaces are stripped out by mucks, I think..
	// TODO use TAB for name autocompletion
	if (e->KeyCode == Keys::Tab) {
		e->SuppressKeyPress = true;
		return;
	}//TAB

	//= keypress for detecting "p =" and "w ="
	if (e->KeyCode == Keys::Oemplus)//'='
	{
		// TODO check for "p =" and "w =" in the middle of multiple lines
		// p =
		if ((input->Text == "p ") || (input->Text == "page "))
		{
			input->Text = "p " + parent->lastpaged;
			input->Select(input->Text->Length,0);
			return;
		}
		// wh =
		if ((input->Text == "w ") || (input->Text == "wh ") || (input->Text == "whisp "))
		{
			input->Text = "wh " + parent->lastwhispered;
			input->Select(input->Text->Length,0);
			return;
		}
	}
	//= keypress for detecting "p =" and "w ="

	// CTRL+UP = go up(back) in command history
	if ((e->Control) && (e->KeyCode == Keys::Up))
	{
		if (history.Count == 0)
			return;

		if (historycurrent > 0)
			historycurrent--;
		else return; // reached end

		input->Text = history[historycurrent];
		input->SelectionStart = input->Text->Length; // select all text

		return;
	}//CTRL+UP

	// CTRL+DOWN = go down(forward) in command history
	if ((e->Control) && (e->KeyCode == Keys::Down))
	{
		if (history.Count == 0)
			return;

		if (historycurrent < history.Count)
			historycurrent++;

		if (historycurrent == history.Count) { // user reached end of history and went one more forward
			// maybe wrap to beginning?.. nah
			input->Text = "";
			return;
		}

		input->Text = history[historycurrent];
		input->SelectionStart = input->Text->Length; // select all text

		return;
	}//CTRL+DOWN
*/
			// ENTER
			if ((!e.Control) && (e.KeyCode == Keys.Enter))
			{
				// UNDONE enable force mode with input
				// if we're in debug mode, pass input line straight to procline
				/*if (force) {
					array<String^> ^ delims = {"\r\n"};
					array<String^> ^ strings = input->Text->Split(delims,StringSplitOptions::None);
					for each (String^ s in strings) {
						parent->procline(s);
					}
					input->Clear();
					e->SuppressKeyPress = true;
					return;
				}*/

				if (input.Text == "") {
					e.SuppressKeyPress = true;
					input.Clear();
					return;
				}

				// post the event to send text
				ESend(this, input.Text);

				// add this line to history
				if (history.Count == 0) { // start new history
					history.Add( input.Text );
					historycurrent = 0;
				}
				else { // add to existing history list
					if (input.Text != history[history.Count - 1]) { // avoid duplicate consecutive history entries
						history.Add( input.Text );
						historycurrent++;
					}
				}

				historycurrent = history.Count;

				e.SuppressKeyPress = true;

				return;

			}//ENTER
		}

		void input_TextChanged(object sender, EventArgs e)
		{
			// TODO convert code for buffer length warning
/*
	// warning when we approach send buffer length
	// this should be per line, not per total inputboxtext
	int x = parent->sendbuffersize - 1 - input->Text->Length;
	if (x < 50)
	{
		this->SuspendLayout();
		warning->Text = x.ToString();
		warning->ForeColor = Color::DarkRed;
		//warning->Font->Bold = false;
		if (x < 10) {
			warning->ForeColor = Color::Red;
			//warning->Font->Bold = true;
		}
		warning->Visible = true;
		this->ResumeLayout();
	}
	else
		warning->Visible = false;
*/
		}
	}
}
