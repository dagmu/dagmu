using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DagMU
{
	public partial class LogSettingsWindow : Form
	{
		public LogSettingsWindow()
		{
			InitializeComponent();
		}

		void buttonLogFolder_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog fd = new FolderBrowserDialog();
			fd.RootFolder = System.Environment.SpecialFolder.Desktop;
			fd.ShowDialog();
			txtLogDir.Text = fd.SelectedPath;
			Properties.Settings.Default.LogDir = fd.SelectedPath;
			Properties.Settings.Default.Save();
		}
	}
}
