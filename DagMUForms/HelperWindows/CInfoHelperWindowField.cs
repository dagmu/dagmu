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
	public partial class CInfoHelperWindowField : UserControl
	{
		/*public CInfoHelperWindowField()
		{
			InitializeComponent();
			Dirty = false;
			Owned = false;
			updating = false;
			MainNotMisc = true;
		}*/

		public CInfoHelperWindowField(String thefieldname, bool mainormisc)
		{
			InitializeComponent();
			Dirty = false;
			Owned = false;
			updating = false;
			textbox.ReadOnly = true;
			FieldName = thefieldname;
			MainNotMisc = mainormisc;
			updatebox();
		}

		bool updating;

		String fieldname;
		public String FieldName
		{
			get { return fieldname; }
			set {
				fieldname = value;
				//updatebox();
			}
		}

		String fieldtext;
		public String FieldText
		{
			get { return fieldtext; }
			set {
				if (Owned)
					textbox.ReadOnly = false;
				fieldtext = value;
				updatebox();
			}
		}

		public bool MainNotMisc;

		bool dirty;
		public bool Dirty
		{
			get { return dirty; }
			set {
				if (value)
					textbox.BackColor = Color.WhiteSmoke;
				else
					textbox.BackColor = BackColor;
				dirty = value;
			}
		}

		bool owned;
		public bool Owned
		{
			get { return owned; }
			set { owned = value; }
		}

		int lines;

		void updatebox()
		{
			int cursorposition = textbox.SelectionStart;
			SuspendLayout();
			updating = true;
			textbox.Clear();
			textbox.ClearUndo();
			updating = true;
			textbox.Font = labelFontNormal.Font;
			updating = true;
			textbox.Text = fieldname + ": " + fieldtext;
			textbox.Select(0, fieldname.Length);
			updating = true;
			textbox.SelectionFont = labelFontBold.Font;
			textbox.Select(cursorposition, 0);
			ResumeLayout();
			SizeBox();
		}

		public void SaveChangesToMuck()
		{
			bool renaming = false;
			bool deleting = false;

			String newfieldname = null;
			String newfieldtext = null;

			if (textbox.Text.Length > 0)
			{
				// check field name for validity, changes
				String[] words;
				Char[] delimbadnamechars = { ' ' };
				Char[] delimspace = { ' ' };
				int colonpos = textbox.Text.IndexOf(':');
				if (colonpos == -1)
				{
					MessageBox.Show("Field: " + fieldname + ", Keep the : between field name and text");
					return;
				}
				newfieldname = textbox.Text.Substring(0, colonpos);
				if (newfieldname.Length > 0)
				{
					words = newfieldname.Split(delimbadnamechars, StringSplitOptions.RemoveEmptyEntries);
					if (words.Length > 1)
					{
						MessageBox.Show("Field: " + fieldname + ", Bad character detected in field name. No spaces allowed.");
						return;
					}
					newfieldname = words[0];
					if (newfieldname != fieldname)
					{
						renaming = true;
					}
				}
				else
					deleting = true;

				// check field text for content
				if (textbox.Text.Length - colonpos == 0)
				{
					deleting = true;
				}
				else
				{
					newfieldtext = textbox.Text.Substring(colonpos + 1, textbox.Text.Length - colonpos - 1);
					words = newfieldtext.Split(delimspace, StringSplitOptions.RemoveEmptyEntries);
					if (words.Length == 0)
					{
						deleting = true;
					}
					while (newfieldtext.StartsWith(" "))
						newfieldtext = newfieldtext.Substring(1);
				}
			}
			else
				deleting = true;

			// handle renaming and deleting
			if (renaming || deleting)
			{
				// erase old field
				// for renaming, field is readded by next part
				ESaveField(this, fieldname, String.Empty);
			}
			if (deleting)
			{
				if (!MainNotMisc)
					fieldname = newfieldname = null;
				fieldtext = newfieldtext = null;
				return;
			}


			fieldname = newfieldname;
			fieldtext = newfieldtext;
	
			// update field text to muck
			//cinfo #set <field>=<text>
			//cinfo #setmisc <fieldname>=<text>
			ESaveField(this, fieldname, fieldtext);

			Dirty = false;
		}

		void textbox_TextChanged(object sender, EventArgs e)
		{
			int cursorpos;

			if (updating)
			{
				updating = false;
				Dirty = false;
				return;
			}
			else
			{
				Dirty = true;
			}

			// make sure to filter out newlines, these fields are not really multiline
			cursorpos = textbox.SelectionStart;
			if (textbox.Text.Contains("\n"))
				textbox.Text = textbox.Text.Replace('\n', ' ');
			if (textbox.Text.Contains("\r"))
				textbox.Text = textbox.Text.Replace('\r', ' ');
			textbox.Select(cursorpos, 0); // keep carat where it was.. ish

			SizeBox();
		}

		void SizeBox()
		{
			// resize to fit text
			Size textsize = TextRenderer.MeasureText(textbox.Text, textbox.Font);
			int textlines = (int)(Math.Ceiling((double)(textsize.Width) / (double)(textbox.ClientSize.Width)));
			int prefheight = textbox.PreferredHeight;
			if (textlines != lines)
			{
				lines = textlines;
				if (Dirty)
					lines++;
				SuspendLayout();
				textbox.Height = lines*prefheight;
				Height = textbox.Top + textbox.Height + textbox.Margin.Top + textbox.Margin.Bottom;
				ResumeLayout();
			}
		}

		void textbox_Click(object sender, EventArgs e)
		{
			// if not expanded, expand first of all
			if (fieldtext == null)
			{
				ERequestField(this, fieldname, null);
				return;
			}

			// if not editable, ignore click
			if (!Owned)
				return;

			// if disabled but editable, go into edit mode
			if (Owned && !textbox.Enabled)
				textbox.Enabled = true;

			// pass click through to textbox
		}

		public delegate void TwoTextMessage(CInfoHelperWindowField sender, String _fieldname, String _fieldtext);
		public event TwoTextMessage ERequestField; // request custom field text for an unexpanded field
		public event TwoTextMessage ESaveField; // submit updated field to the muck

		public delegate void ScrollMessage(MouseEventArgs mouseeventargs);
		public event ScrollMessage EScroll; // pass mousewheel messages to main window

		void textbox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EScroll(e);
		}

		void textbox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}
	}
}
