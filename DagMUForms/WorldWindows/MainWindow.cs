using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DagMU.Forms.HelperWindows;

namespace DagMU.Forms
{
	public partial class MainWindow : Form//DagGlassLib.GlassForm
	{
		List<World> worlds = new List<World>(); // Collection of worlds
		World currentworld; // Index of currently selected world in worlds
		int worldindex = 0; // primary key for worlds

		public MainWindow()
		{
			InitializeComponent();
		}

		void OnWorldResize(object sender, EventArgs e)
		{
			World w = (World)sender;

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

		void MainWindow_Load(object sender, EventArgs e)
		{
			this.Size = DagMU.Forms.Properties.Settings.Default.MainLastSize;

			tbnForceLocal_Click(null, null);// HACK 
		}

		void WorldAdd(World.WorldSettings settings, World.WorldPrefs prefs)
		{
			World w = new World(worldindex++);
			worlds.Add(w);
			currentworld = w;

			w.settings = settings;
			w.prefs = prefs ?? new World.WorldPrefs();

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

			w.Connect();
		}

		void OnWorldDisconnected(object sender, EventArgs e)
		{
			if (sender as World == currentworld)
				tbnConnectEnabled(true);
		}

		void OnWorldConnecting(object sender, EventArgs e)
		{
		}

		void OnWorldConnected(object sender, EventArgs e)
		{
			if (sender as World == currentworld)
				tbnConnectEnabled(false);
		}

		void OnWorldClosing(object sender, EventArgs e)
		{
			World world = sender as World;
			if (world == currentworld)
				tbnConnectEnabled(true);
			world.Hide();
			Controls.Remove(world);
			worlds.Remove(world);
		}

		World CurrentWorld { get { return worlds.SingleOrDefault(x => x == currentworld); } }

		void tbnForceLocal_Click(object sender, EventArgs e)
		{
			WorldAdd(
				new World.WorldSettings() {
					Address = "localhost",
					Port = 2069,
					NameFull = "Localhost",
					NameShort = "LH",
					sendbuffersize = 4096,
					sendQUITstring = "QUIT",
				},
				new World.WorldPrefs() {
				}
			);
		}

		void tbnTaps_Click(object sender, EventArgs e)
		{
			WorldAdd(
				tapsSettings(),
				new World.WorldPrefs() {
				}
			);
		}

		World.WorldSettings tapsSettings()
		{
			return new World.WorldSettings() {
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
				sendbuffersize = 4096,
				sendQUITstring = "QUIT",
			};
		}

		void tbnConnect_Click(object sender, EventArgs e)
		{
			World w = CurrentWorld;

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
