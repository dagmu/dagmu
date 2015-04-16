using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
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

		String fieldName;
		public String FieldName
		{
			get { return fieldName; }
			set {
				fieldName = value;
				//updatebox();
			}
		}

		String fieldText;
		public String FieldText
		{
			get { return fieldText; }
			set {
				if (Owned)
					textbox.ReadOnly = false;
				fieldText = value;
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
			textbox.Text = fieldName + ": " + fieldText;
			textbox.Select(0, fieldName.Length);
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

			if (textbox.Text.Length > 0) {
				// check field name for validity, changes
				String[] words;
				Char[] delimbadnamechars = { ' ' };
				Char[] delimspace = { ' ' };
				int colonpos = textbox.Text.IndexOf(':');
				if (colonpos == -1) {
					MessageBox.Show("Field: " + fieldName + ", Keep the : between field name and text");
					return;
				}
				newfieldname = textbox.Text.Substring(0, colonpos);
				if (newfieldname.Length > 0) {
					words = newfieldname.Split(delimbadnamechars, StringSplitOptions.RemoveEmptyEntries);
					if (words.Length > 1) {
						MessageBox.Show("Field: " + fieldName + ", Bad character detected in field name. No spaces allowed.");
						return;
					}
					newfieldname = words[0];
					if (newfieldname != fieldName)
						renaming = true;
				} else
					deleting = true;

				if (textbox.Text.Length - colonpos == 0) {//check field text for content
					deleting = true;
				} else {
					newfieldtext = textbox.Text.Substring(colonpos + 1, textbox.Text.Length - colonpos - 1);
					words = newfieldtext.Split(delimspace, StringSplitOptions.RemoveEmptyEntries);
					if (words.Length == 0) {
						deleting = true;
					}
					while (newfieldtext.StartsWith(" "))
						newfieldtext = newfieldtext.Substring(1);
				}
			} else {
				deleting = true;
			}

			if (renaming || deleting) {
				// erase old field
				// for renaming, field is readded by next part
				ESaveField(this, new Tuple<string, string>(fieldName, String.Empty));
			}

			if (deleting) {
				if (!MainNotMisc)
					fieldName = newfieldname = null;
				fieldText = newfieldtext = null;
				return;
			}

			fieldName = newfieldname;
			fieldText = newfieldtext;

			// update field text to muck
			//cinfo #set <field>=<text>
			//cinfo #setmisc <fieldname>=<text>
			ESaveField(this, new Tuple<string, string>(fieldName, fieldText));

			Dirty = false;
		}

		void textbox_TextChanged(object sender, EventArgs e)
		{
			int cursorpos;

			if (updating) {
				updating = false;
				Dirty = false;
				return;
			} else {
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
			if (textlines != lines) {
				lines = textlines;
				if (Dirty)
					lines++;
				SuspendLayout();
				textbox.Height = lines * prefheight;
				Height = textbox.Top + textbox.Height + textbox.Margin.Top + textbox.Margin.Bottom;
				ResumeLayout();
			}
		}

		void textbox_Click(object sender, EventArgs e)
		{
			// if not expanded, expand first of all
			if (fieldText == null) {
				ERequestField(this, new Tuple<string, string>(fieldName, null));
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

		/// <summary>
		/// request custom field text for an unexpanded field
		/// CInfoHelperWindowField sender, String _fieldname, String _fieldtext
		/// </summary>
		public event EventHandler<Tuple<string, string>> ERequestField;

		/// <summary>
		/// submit updated field to the muck
		/// CInfoHelperWindowField sender, String _fieldname, String _fieldtext
		/// </summary>
		public event EventHandler<Tuple<string, string>> ESaveField;

		public event EventHandler<MouseEventArgs> EScroll; // pass mousewheel messages to main window

		void textbox_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			EScroll(sender, e);
		}

		void textbox_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}
	}
}
