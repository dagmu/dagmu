using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DagMU.WorldWindows
{
	public partial class WorldToolStripMenuItem : UserControl
	{
		public override String Text { get { return WorldName + " · " + CharName; } }

		public String WorldName = "Taps";
		public String CharName = "Dagon";
		public bool Connected = true;

		public WorldToolStripMenuItem()
		{
			InitializeComponent();
		}
	}
}
