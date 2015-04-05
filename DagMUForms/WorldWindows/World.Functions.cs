using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DagMU.HelperWindows;

namespace DagMU
{
	partial class World
	{
		public World(int myindex)
		{
			index = myindex;
			charname = null;

			connection = new MuckConnection();
			connection.EConnect += new MuckConnection.ConnectEventHandler(OnConnect);
			connection.ERead += new MuckConnection.ReadEventHandler(OnRead);
			connection.ESend += new MuckConnection.SendEventHandler(OnSend);

			InitializeComponent();

			boxofinputboxes.Resize += OnInputBoxesResize;
			boxofinputboxes.EWantsToSend += new InputBox.TextMessage(OnInputBoxesHasTextToSend);
			OnInputBoxesResize(null, null);

			tbnRideMode.ESelect += new StringDelegate(OnRideModeSelected);

			HelperWindows = new List<HelperWindow>();
			CInfoHelperWindows = new List<CInfoHelperWindow>();

			Console = new HelperWindows.ConsoleWindow();
			HelperWindows.Add(Console);
			Console.Show();

			wf = new WF(this);

			logsettings = null;

			debugwindow = new DebugWindow();
			debugwindow.ESend += debugwindow_ESend;
			debugwindow.EStatusReset += debugwindow_EStatusReset;
			HelperWindows.Add(debugwindow);
	
			DescEditor = new DescEditorWindow();
			HelperWindows.Add(DescEditor);

			MorphHelper = new MorphHelperWindow();
			HelperWindows.Add(MorphHelper);

			WIHelper = new WIHelperWindow();
			HelperWindows.Add(WIHelper);

			FontsColors = new FontsColorsWindow();
			HelperWindows.Add(FontsColors);

			foreach (HelperWindow w in HelperWindows)
			{
				w.parent = this;
				w.Hide();
			}

			boxofmucktext.BackColor = global::DagMU.Properties.Settings.Default.BoxForeColor;
			boxofmucktext.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::DagMU.Properties.Settings.Default, "BoxForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
	
			boxofmucktext.BackColor = global::DagMU.Properties.Settings.Default.BoxBackColor;
			boxofmucktext.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::DagMU.Properties.Settings.Default, "BoxBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			boxofmucktext.Font = global::DagMU.Properties.Settings.Default.Font;
			boxofmucktext.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DagMU.Properties.Settings.Default, "BoxFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
		}

		List<HelperWindow> HelperWindows;

		List<CInfoHelperWindow> CInfoHelperWindows;

		void OnInputBoxesHasTextToSend(InputBox sender, string msg)
		{
			Send(msg, sender);
		}

		/// <summary>
		/// Input box(es) have resized
		/// Resize world window to fit
		/// Triggers MainWindow resize event to resize actual window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void OnInputBoxesResize(object sender, EventArgs e)
		{
			SuspendLayout();

			boxofmucktext.Anchor = AnchorStyles.None;
			boxofmucktext.Top = menu.Bottom;

			boxofinputboxes.Top = boxofmucktext.Bottom;// +Padding.Bottom;

			Height = menu.Height + boxofmucktext.Height + boxofinputboxes.Height;// +Padding.Vertical + Padding.Vertical;

			boxofmucktext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

			ResumeLayout();
		}

		void OnConnect(MuckConnection.ConnectEvent status, string message)
		{
			switch (status) {
				case MuckConnection.ConnectEvent.Connected:
					newstatus(MuckStatus.intercepting_connecting, message);
					EConnected(this);
					break;

				case MuckConnection.ConnectEvent.Got_Disconnected:
				case MuckConnection.ConnectEvent.Error_Connecting:
					EDisconnected(this);
					newstatus(MuckStatus.not_connected, message);
					break;
			}
		}

		CInfoHelperWindow FindCInfoWindow(String charactername)
		{
			return CInfoHelperWindows.FirstOrDefault(x => x.CharName == charactername);
		}

		CInfoHelperWindow MakeCInfoWindow(String charactername)
		{
			CInfoHelperWindow cwin = new CInfoHelperWindow(charactername, CharName == charactername);
			HelperWindows.Add(cwin);
			CInfoHelperWindows.Add(cwin);
			cwin.parent = this;
			cwin.FormClosing += CInfoHelper_FormClosing;
			cwin.Show();
			cwin.BringToFront();
			return cwin;
		}

		CInfoHelperWindow FindMakeCInfoWindow(String charactername)
		{
			CInfoHelperWindow cwin = FindCInfoWindow(charactername);
			if ( cwin != null ) {
				cwin.Show();
				cwin.BringToFront();
				return cwin;
			}

			return MakeCInfoWindow(charactername);
		}

		void CInfoHelper_FormClosing(object sender, FormClosingEventArgs e)
		{
			HelperWindows.Remove((CInfoHelperWindow)sender);
			CInfoHelperWindows.Remove((CInfoHelperWindow)sender);
		}

		void OnRead(string text)
		{
			procline(text);
			Console.Print(text + '\n');
		}

		void OnSend(MuckConnection.SendStatus status, object inputbox, string errarMessage = null)
		{
			switch (status)
			{
				case MuckConnection.SendStatus.send_error:
					boxprint("Send errar! " + errarMessage ?? "");
					if (inputbox != null) ((InputBox)inputbox).newstatus(InputBox.Status.Disconnected);
					Disconnect();
					break;

				case MuckConnection.SendStatus.sent:
					if (inputbox != null) ((InputBox)inputbox).newstatus(InputBox.Status.Sent);
					break;
			}
		}

		public async void Connect()
		{
			if (connection.Connected)
				return;

			boxprint("Connecting to " + settings.NameFull + " [" + settings.Address + " : " + settings.Port + "]");

			try {
				await connection.Connect(settings.Address, settings.Port, settings.SSL, settings.CertificateHash);
			} catch (Exception e) {
				boxprint("Error connecting: " + e.Message);
			}
		}

		public void Disconnect()
		{
			connection.Disconnect();
			EDisconnected(this);
			newstatus(MuckStatus.not_connected);
		}

		public void Send(String line, InputBox ib)
		{
			if ((status != MuckStatus.intercepting_normal) && (ib == null)) return;//only send automatic stuff to the muck if we're in normal mode, so clicking buttons won't cause problems when the muck isn't ready for it

			connection.Send(line, ib);
			Console.Print("]" + line + "\n");
		}

		public void boxprint(String s)
		{
			if (this.ParentForm == null) return;

			if (!this.ParentForm.Visible)
				User32.FlashWindow(this.ParentForm.Handle, true);

			// if we're in the wrong thread, pass the message to the right thread
			if ( boxofmucktext.InvokeRequired ) {
				StringDelegate callback = new StringDelegate(boxprint);
				try {
					this.Invoke(callback, s); // have the world invoke it on the right thread, calling callback(s) doesn't come back on the right thread
				} catch { }
				return;
			}

			Log(s);

			boxofmucktext.Add(s);
		}

		void newstatus(MuckStatus newstatus, string message = null)
		{
			//debugwindow.Text = ((int)newstatus).ToString();

			if (newstatus == MuckStatus.not_connected) {
				boxprint(String.Join(": ", "Disconnected.", message));

				// tell input boxes to grey out or go away. grey out if there is text untyped, go away if they are empty
				boxofinputboxes.UpdateStatus(InputBox.Status.Disconnected);
			}

			status = newstatus;
			if (debugwindow != null)
				debugwindow.UpdateStatus(newstatus.ToString());
		}

		void boxecho(String s)
		{
			connection.Send("dagmuecho dagmu_echo " + s, null);// should add a random number check here that we pick at startup
		}

		bool isecho(String s)
		{
			// should add a random number check here that we pick at startup
			// see boxecho()
			if (s.StartsWith("dagmu_echo "))
				return true;
			return false;
		}

		bool isecho(String s, String message)
		{
			if (!isecho(s))
				return false;

			if (s.Substring(11) == message)
				return true;

			return false;
		}

		void OnRideModeSelected(String ridemode)
		{
			Send("@set me=/ride/_mode:" + ridemode, null);//TAPS
			refocus();
		}
	}
}
