using System;
using System.Windows.Forms;

namespace DagMU.Forms.HelperWindows
{
	public partial class FontsColorsWindow : DagMU.Forms.HelperWindows.HelperForm
	{
		public FontsColorsWindow()
		{
			InitializeComponent();

			PresetButton.PresetButtonPointers buttonpointers = new PresetButton.PresetButtonPointers();
			buttonpointers.btnBoxFore = btnBoxFore;
			buttonpointers.btnBoxBack = btnBoxBack;
			buttonpointers.btnInputFore = btnInputFore;
			buttonpointers.btnInputBack = btnInputBack;
			btnAsh.buttonpointers = btnChill.buttonpointers
				= btnTerminal.buttonpointers = btnNotepad.buttonpointers = buttonpointers;
		}

		void btnCancel_Click(object sender, EventArgs e)
		{
			DagMU.Forms.Properties.Settings.Default.Reload();
			Close();
		}

		void btnApply_Click(object sender, EventArgs e)
		{
			DagMU.Forms.Properties.Settings.Default.Save();
			Close();
		}
		class PresetButton : Button
		{
			public class PresetButtonPointers
			{
				public Button btnBoxFore;
				public Button btnBoxBack;
				public Button btnInputFore;
				public Button btnInputBack;
			}
			public PresetButtonPointers buttonpointers;

			public PresetButton()
			{
				Click += new EventHandler(PresetButton_Click);

				BoxFont = Font;
				BoxBack = InputBack = BackColor;
				BoxFore = InputFore = ForeColor;
			}

			void PresetButton_Click(object sender, EventArgs e)
			{
				if (buttonpointers == null)
					return;

				buttonpointers.btnBoxFore.BackColor = BoxFore;
				buttonpointers.btnBoxBack.ForeColor = BoxFore;
				buttonpointers.btnBoxBack.BackColor = BoxBack;
				buttonpointers.btnBoxBack.Font = BoxFont;
				buttonpointers.btnInputFore.BackColor = InputFore;
				buttonpointers.btnInputBack.BackColor = InputBack;
			}

			System.Drawing.Font boxfont;
			public System.Drawing.Font BoxFont
			{
				get { return boxfont; }
				set
				{
					boxfont = value;
					Font = value;
				}
			}

			System.Drawing.Color boxfore;
			public System.Drawing.Color BoxFore
			{
				get { return boxfore; }
				set
				{
					boxfore = value;
					ForeColor = value;
				}
			}

			System.Drawing.Color boxback;
			public System.Drawing.Color BoxBack
			{
				get { return boxback; }
				set
				{
					boxback = value;
					BackColor = value;
				}
			}

			System.Drawing.Color inputfore;
			public System.Drawing.Color InputFore
			{
				get { return inputfore; }
				set { inputfore = value; }
			}

			System.Drawing.Color inputback;
			public System.Drawing.Color InputBack
			{
				get { return inputback; }
				set { inputback = value; }
			}
		}

		void rbFontLucidaConsole_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFontLucidaConsole.Checked)
				btnBoxBack.Font = new System.Drawing.Font(rbFontLucidaConsole.Font.FontFamily, btnBoxBack.Font.Size);
		}

		void rbFontLucidaSans_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFontLucidaSans.Checked)
				btnBoxBack.Font = new System.Drawing.Font(rbFontLucidaSans.Font.FontFamily, btnBoxBack.Font.Size);
		}

		void rbFontCourierNew_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFontCourierNew.Checked)
				btnBoxBack.Font = new System.Drawing.Font(rbFontCourierNew.Font.FontFamily, btnBoxBack.Font.Size);
		}

		void rbFontConsolas_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFontConsolas.Checked)
				btnBoxBack.Font = new System.Drawing.Font(rbFontConsolas.Font.FontFamily, btnBoxBack.Font.Size);
		}

		void rbFontX_CheckedChanged(object sender, EventArgs e)
		{
			if (rbFontX.Checked)
				btnBoxBack.Font = new System.Drawing.Font(fontDialog.Font.FontFamily, btnBoxBack.Font.Size);
		}

		void btnFont_Click(object sender, EventArgs e)
		{
			fontDialog.ShowDialog();
			rbFontX.Checked = true;
		}

		void fontDialog_Apply(object sender, EventArgs e)
		{
			if (fontDialog.Font.Size < (int)numSize.Minimum)
				fontDialog.Font = new System.Drawing.Font(fontDialog.Font.FontFamily, (int)numSize.Minimum);
			if (fontDialog.Font.Size > (int)numSize.Maximum)
				fontDialog.Font = new System.Drawing.Font(fontDialog.Font.FontFamily, (int)numSize.Maximum);

			btnBoxBack.Font = fontDialog.Font;

			if (fontDialog.Font.Size == 10)
				rbSize10.Checked = true;
			else if (fontDialog.Font.Size == 11)
				rbSize11.Checked = true;
			else if (fontDialog.Font.Size == 12)
				rbSize12.Checked = true;
			else if (fontDialog.Font.Size == 14)
				rbSize14.Checked = true;
			else
			{
				numSize.Value = (decimal)fontDialog.Font.Size;
				rbSizeX.Checked = true;
			}
		}

		void rbSize10_CheckedChanged(object sender, EventArgs e)
		{
			if (rbSize10.Checked)
				btnBoxBack.Font = new System.Drawing.Font(btnBoxBack.Font.FontFamily, 10);
		}

		void rbSize11_CheckedChanged(object sender, EventArgs e)
		{
			if (rbSize11.Checked)
				btnBoxBack.Font = new System.Drawing.Font(btnBoxBack.Font.FontFamily, 11);
		}

		void rbSize12_CheckedChanged(object sender, EventArgs e)
		{
			if (rbSize12.Checked)
				btnBoxBack.Font = new System.Drawing.Font(btnBoxBack.Font.FontFamily, 12);
		}

		void rbSize14_CheckedChanged(object sender, EventArgs e)
		{
			if (rbSize14.Checked)
				btnBoxBack.Font = new System.Drawing.Font(btnBoxBack.Font.FontFamily, 14);
		}

		void rbSizeX_CheckedChanged(object sender, EventArgs e)
		{
			if (rbSizeX.Checked)
				btnBoxBack.Font = new System.Drawing.Font(btnBoxBack.Font.FontFamily, (int)numSize.Value);
		}

		void numSize_ValueChanged(object sender, EventArgs e)
		{
			if (rbSizeX.Checked)
				btnBoxBack.Font = new System.Drawing.Font(btnBoxBack.Font.FontFamily, (int)numSize.Value);
		}

		void btnBoxFore_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog.ShowDialog();
			btnBoxFore.BackColor = colorDialog.Color;
		}

		void btnBoxBack_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog.ShowDialog();
			btnBoxBack.BackColor = colorDialog.Color;
		}

		void btnInputFore_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog.ShowDialog();
			btnInputFore.BackColor = colorDialog.Color;
		}

		void btnInputBack_Click(object sender, EventArgs e)
		{
			DialogResult result = colorDialog.ShowDialog();
			btnInputBack.BackColor = colorDialog.Color;
		}
	}
}
