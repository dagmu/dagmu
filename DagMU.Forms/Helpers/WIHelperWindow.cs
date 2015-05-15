using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
{
	public partial class WIHelperWindow : DagMU.Forms.Helpers.HelperForm
    {
        public WIHelperWindow()
        {
            InitializeComponent();
        }

		public void addstarting()
		{
			text.Clear();
		}

		public void addItem(string data)
		{
			if (String.IsNullOrEmpty(data))
				return;

			if (data == "!:no")
				return;

			text.AppendText(data + "\r\n");

			// make sure it doesn't already exist
			System.Windows.Forms.CheckBox cbox;

			int colonindex = data.IndexOf(':');
			//pp:public-propert
			//String ^ s1 = data.Substring(0, colonindex);	//pp
			//String ^ s2 = data.Substring(colonindex+1);	//public-propert

			// add it to the box
			cbox = new System.Windows.Forms.CheckBox();
			cbox.Name = data.Substring(0, colonindex);//pp	[Name is unseen]
			cbox.Text = data.Substring(colonindex+1);//pp:public-propert
			cbox.ThreeState = true;

			box.Controls.Add(cbox);
		}

		public void adddone()
		{
			try
			{
				text.SaveFile("wiflags.txt",RichTextBoxStreamType.PlainText);
			}
			catch (System.IO.IOException)
			{
				MessageBox.Show("Errar, can't save wiflags.txt");
			}
			text.Clear();//free up ram
			text.ClearUndo();//free up a little ram maybe
			Show();
		}

		public void updatecustom(string data)
		{
			//"oftenshapeshifted cinfo-reader"
			textBoxCustom.Text = data;
			checkBoxCustom.Checked = true;
		}

		public void updateflags(string data)
		{
			// UNDONE we receive the flag settings
			//"bi d sw ic cin rom prd int"

			// check the appropriate boxes

			// split data by spacedelim
			String[] delimspace = {" "};
			String[] words = data.Split(delimspace,StringSplitOptions.RemoveEmptyEntries);

			foreach (String word in words)
			{
				// check box with that name, if found
				CheckBox cbox = (CheckBox)box.Controls[word];

				cbox.Checked = true;
			}
		}

		void WIHelperWindow_Load(object sender, EventArgs e)
		{
			// load text file if theres no stuff loaded already

			if (box.Controls.Count > 0)
			{
				//MessageBox::Show("already filled in, skipping text file load");
				return;
			}

			try
			{
				text.LoadFile("wiflags.txt",RichTextBoxStreamType.PlainText);
			}
			catch ( System.IO.IOException) 
			{
				MessageBox.Show("Errar: Could not open wiflags.txt, try typing 'wi #flags' to the muck and this file should be written." + e.GetType().Name );
				return;
			}

			box.Controls.Clear();

			// load from the textbox, which is the file

			foreach (String line in text.Lines)
				addItem(line);

			text.Clear();
			text.ClearUndo();
		}

		void buttonSave_Click(object sender, EventArgs e)
		{
			if (!checkBoxCustom.Checked)
				parent.Send("@set me=/_prefs/whatis/custom:", null);
			else
				parent.Send("@set me=/_prefs/whatis/custom:" + textBoxCustom.Text, null);

			String line = "";

			foreach (CheckBox cbox in box.Controls)
			{
				if (cbox.CheckState == System.Windows.Forms.CheckState.Checked)
				{
					if (line.Length == 0)
						line = cbox.Name;
					else
						line += " " + cbox.Name;
					continue;
				}
				if (cbox.CheckState == System.Windows.Forms.CheckState.Indeterminate)
				{
					if (line.Length == 0)
						line = "!" + cbox.Name;
					else
						line += " !" + cbox.Name;
					continue;
				}
			}

			MessageBox.Show(line);
			parent.Send("@set me=/_prefs/whatis/flags:" + line, null);

		}

		void WIHelperWindow_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			MessageBox.Show(
				"This window lets you easily change your WhatIs settings\n\n"+

				"How this window works, behind the scenes:\n\n"+

				"The refresh button requests /_prefs/whatis/used, /custom, /flags properties to populate the window.\n\n"+

				"Flags are read from a local copy of the 'wi #flags' so the muck isn't bogged down by lots of requests. When you do 'wi #flags' it refreshes this text file.\n\n"+

				"DagMU does not intercept the normal WI readout for this window, and currently doesn't support wi #detail."

				,"WI Editor Help"
			);
			e.Cancel = true;
		}
    }
}
