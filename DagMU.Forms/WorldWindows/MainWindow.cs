using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DagMU.Forms.Helpers;
using System.Threading.Tasks;

namespace DagMU.Forms
{
	public partial class MainWindow : Form//DagGlassLib.GlassForm
	{
		List<WorldVM> worlds = new List<WorldVM>(); // Collection of worlds
		WorldVM currentworld; // Index of currently selected world in worlds
		int worldindex = 0; // primary key for worlds

		public MainWindow()
		{
			InitializeComponent();
		}

		void MainWindow_Load(object sender, EventArgs e)
		{
			this.Size = DagMU.Forms.Properties.Settings.Default.MainLastSize;

			//window was AWOL once, so make sure it's got sane size
			this.Height = Math.Max(300, this.Height);
			this.Width = Math.Max(400, this.Width);
			this.Left = Math.Max(0, this.Left);
			this.Top = Math.Max(0, this.Top);

#if DEBUG
			tbnForceLocal_Click(null, null);// HACK
#endif
		}

		void OnWorldResize(object sender, EventArgs e)
		{
			WorldVM w = (WorldVM)sender;

			SuspendLayout();

			System.Drawing.Size newsize = new System.Drawing.Size(ClientSize.Width, w.Height + w.Top + w.Margin.Top + w.Margin.Bottom);
			ClientSize = newsize;

			ResumeLayout();
		}

		void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			User32.FlashWindow(Handle, cbFlash.Checked);
		}

		void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			DagMU.Forms.Properties.Settings.Default.MainLastSize = this.Size;
			DagMU.Forms.Properties.Settings.Default.Save();
		}

		void WorldVMAdd(WorldVM.WorldSettings settings, WorldVM.WorldPrefs prefs)
		{
			WorldVM w = new WorldVM(worldindex++);
			worlds.Add(w);
			currentworld = w;

			w.settings = settings;
			w.prefs = prefs ?? new WorldVM.WorldPrefs();

			w.Left = 0;
			w.Top = 0;
			w.Width = ClientRectangle.Width;
			w.Height = ClientRectangle.Height - w.Top;
			w.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
			w.Resize += new EventHandler(OnWorldResize);
			Controls.Add(w);

			w.EClosing += OnWorldClosing;
			w.EConnected += OnWorldConnected;
			w.EDisconnected += OnWorldDisconnected;

			Task.Run(() => w.Connect());
		}

		void OnWorldDisconnected(object sender, EventArgs e)
		{
			if (sender as WorldVM == currentworld)
				tbnConnectEnabled(true);
		}

		void OnWorldConnecting(object sender, EventArgs e)
		{
		}

		void OnWorldConnected(object sender, EventArgs e)
		{
			if (sender as WorldVM == currentworld)
				tbnConnectEnabled(false);
		}

		void OnWorldClosing(object sender, EventArgs e)
		{
			WorldVM world = sender as WorldVM;
			if (world == currentworld)
				tbnConnectEnabled(true);
			world.Hide();
			Controls.Remove(world);
			worlds.Remove(world);
		}

		WorldVM CurrentWorld { get { return worlds.SingleOrDefault(x => x == currentworld); } }

		void tbnForceLocal_Click(object sender, EventArgs e)
		{
			WorldVMAdd(
				new WorldVM.WorldSettings() {
					Address = "localhost",
					Port = 2069,
					NameFull = "Localhost",
					NameShort = "LH",
					sendBufferSize = 4096,
					sendQUITstring = "QUIT",
				},
				new WorldVM.WorldPrefs() {
				}
			);
		}

		void tbnTaps_Click(object sender, EventArgs e)
		{
			WorldVMAdd(
				tapsSettings(),
				new WorldVM.WorldPrefs() {
				}
			);
		}

		WorldVM.WorldSettings tapsSettings()
		{
			return new WorldVM.WorldSettings() {
				//connection settings
				Address = "tapestries.fur.com",
				Port = 6699,//ssl
				SSL = true,
				CertificateHash = "A194190F76E65269E0D4354CF2BC34A840F5FD47",
				//Port = 2069,//plaintext
				//SSL = false,

				//world settings
				NameFull = "Tapestries",
				NameShort = "Taps",
				sendBufferSize = 4096,
				sendQUITstring = "QUIT",
			};
		}

		void tbnConnect_Click(object sender, EventArgs e)
		{
			WorldVM w = CurrentWorld;

			if (w == null)
				return;

			if (w.Connected)
				w.Connect();
		}

		void tbnConnectEnabled(bool value)
		{
			Invoke((Action)(() => tbnConnect.Enabled = value ));
		}
    }
}
