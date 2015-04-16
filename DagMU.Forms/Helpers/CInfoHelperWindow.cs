using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
{
	public partial class CInfoHelperWindow : HelperForm
	{
		public CInfoHelperWindow(String _charactername, bool _owned)
		{
			InitializeComponent();
			CharName = _charactername;
			Owned = _owned;
			fields = new List<CInfoHelperWindowField>();
			flowLayoutPanel1.Focus();
		}

		List<CInfoHelperWindowField> fields;

		bool owned;
		public bool Owned
		{
			get { return owned; }
			set
			{
				owned = value;
				buttonSave.Visible = value;
			}
		}

		String charname;
		public String CharName
		{
			get { return charname; }
			set
			{
				charname = value;
				labelCharName.Text = value;
			}
		}

		public void UpdateField(String fieldname, String text, bool ismiscfield)
		{
			CInfoHelperWindowField field = FindAddField(fieldname, ismiscfield);
			field.FieldText = text;
			Invalidate();
		}

		public void UpdateMiscFields(String[] fieldnames)
		{
			foreach (String textfield in fieldnames)
				FindAddField(textfield, true);
		}

		CInfoHelperWindowField FindAddField(String fieldname, bool ismiscfield)
		{
			return fields.FirstOrDefault(x => x.FieldName.ToUpper() == fieldname.ToUpper())
				?? AddNewField(fieldname, ismiscfield, Owned);
		}

		CInfoHelperWindowField AddNewField(String fieldname, bool ismiscfield, bool _owned)
		{
			CInfoHelperWindowField newfield = new CInfoHelperWindowField(fieldname, !ismiscfield);
			newfield.ERequestField += newfield_ERequestField;
			newfield.ESaveField += newfield_ESaveField;
			newfield.EScroll += newfield_EScroll;
			newfield.Owned = _owned;
			flowLayoutPanel1.Controls.Add(newfield);
			fields.Add(newfield);
			return newfield;
		}

		void newfield_EScroll(object sender, MouseEventArgs e)
		{
			User32.SendMouseWheelEvent(flowLayoutPanel1, e.Delta);
		}

		void newfield_ESaveField(object cinfoHelperField, Tuple<string, string> field_Name_Text)
		{
			CInfoHelperWindowField field = cinfoHelperField as CInfoHelperWindowField;
			string fieldName = field_Name_Text.Item1;
			string fieldText = field_Name_Text.Item2;
			if (field.MainNotMisc)
				parent.Send("cinfo #set " + fieldName + "=" + fieldText, null);// TAPS cinfo #set field=text
			else
				parent.Send("cinfo #setmisc " + fieldName + "=" + fieldText, null);// TAPS cinfo #setmisc field=text
		}

		void newfield_ERequestField(object cinfoHelperField, Tuple<string, string> field_Name_Text)
		{
			parent.Send("cinfo " + CharName + " " + field_Name_Text.Item1, null);// TAPS cinfo dagon miscfield
		}
		
		void buttonSave_Click(object sender, EventArgs e)
		{
			bool loop = true;
			CInfoHelperWindowField deletingfield = null;
			while (loop)
			{
				loop = false;
				foreach (CInfoHelperWindowField field in fields)
				{
					if (field.Owned && field.Dirty)
					{
						field.SaveChangesToMuck();// save changes to muck

						if (field.FieldName == null)
						{
							deletingfield = field;
							loop = true;
							break;
						}
					}
				}
				if (deletingfield != null)
				{
					flowLayoutPanel1.Controls.Remove(deletingfield);
					fields.Remove(deletingfield);
				}
			}
		}

		void buttonNewMiscField_Click(object sender, EventArgs e)
		{
			CInfoHelperWindowField field = AddNewField("new", true, Owned);
			field.FieldText = " ";
		}
	}
}
