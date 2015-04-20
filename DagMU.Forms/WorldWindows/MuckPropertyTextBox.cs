using System;
using System.Windows.Forms;

namespace DagMU.Forms
{
	public partial class MuckPropertyTextBox : UserControl
	{
		public MuckPropertyTextBox()
		{
			InitializeComponent();
			updating = false;
		}

		public string ListName { get; set; }

		public bool Checked
		{
			get { return checkbox.Checked; }
			set { checkbox.Checked = value; }
		}

		public bool Multiline
		{
			get { return textbox.Multiline; }
			set
			{
				textbox.Multiline = value;
				if (value)
					textbox.ScrollBars = ScrollBars.Vertical;
				else
					textbox.ScrollBars = ScrollBars.None;
			}
		}

		public string Title
		{
			get { return checkbox.Text; }
			set { checkbox.Text = value; }
		}

		public string Input
		{
			get { return textbox.Text; }
			set { textbox.Text = value; }
		}

		private String _muckcommandtoget;
		public String MuckCommandToGet
		{
			get {
				if (!String.IsNullOrEmpty(ListName))
					return "exa me=/" + ListName + "#/";
				else
					return _muckcommandtoget;
			}
			set { _muckcommandtoget = value; }
		}

		private String _muckcommandtoset;
		public String MuckCommandToSet
		{
			get { return _muckcommandtoset; }
			set { _muckcommandtoset = value; }
		}

		private bool _notreallymultiline;
		public bool MultiLineNotReally
		{
			get { return _notreallymultiline; }
			set { _notreallymultiline = value; }
		}

		private bool updating;

		public void GotUpdate(String newtext)
		{
			Enabled = true;
			updating = true;
			textbox.Text = newtext;
		}

		public void GotUpdate(String newtext, int linenum, string listName)
		{
			this.ListName = listName;

			if (linenum < 0) return;

			Enabled = true;
			updating = true;
			if (linenum == 1) {
				textbox.Clear();
			}
			String[] newlines = new String[Math.Max(textbox.Lines.Length, linenum)];
			for (int i = 0; i < newlines.Length; i++) {
				if (i == linenum - 1) {
					newlines[i] = newtext;
				} else if (i > textbox.Lines.Length) {
					return;
				} else {
					newlines[i] = textbox.Lines[i];
				}
			}
			textbox.Lines = newlines;
		}

		private void textbox_TextChanged(object sender, EventArgs e)
		{
			if (updating) {
				updating = false;
				return;
			}

			if (!Checked)
				Checked = true;

			// make sure to filter out newlines
			if (_notreallymultiline) {
				if (textbox.Text.Contains("\n"))
					textbox.Text = textbox.Text.Replace('\n', ' ');
				if (textbox.Text.Contains("\r"))
					textbox.Text = textbox.Text.Replace('\r', ' ');
				textbox.Select(textbox.Text.Length, 0); // keep carat at end
			}
		}
	}
}
