using System;
using System.Windows.Forms;

namespace DagMU.HelperWindows
{
	class RideModeDropdown : ToolStripComboBox
	{
		public RideModeDropdown()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Select the currently enabled ride mode
		/// </summary>
		/// <param name="newmode">Current ride mode string</param>
		public void GotUpdate(String newmode)
		{
			int index = Items.IndexOf(newmode);

			// Not found in dropdown, add it
			if (index == -1)
			{
				index = Items.Add(newmode);
			}

			SelectedIndex = index;
		}

		public event DagMU.World.StringDelegate ESelect;

		private void RideModeDropdown_SelectedIndexChanged(object sender, EventArgs e)
		{
			ESelect(Items[SelectedIndex].ToString());
		}

		private void InitializeComponent()
		{
			this.SelectedIndexChanged += new EventHandler(RideModeDropdown_SelectedIndexChanged);
		}
	}
}
