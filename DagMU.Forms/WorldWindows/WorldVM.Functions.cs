using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using DagMU.Model;
using DagMU.Forms.Helpers;
using DagMUWPF.Windows;
using System.Windows.Forms.Integration;

namespace DagMU.Forms
{
	partial class WorldVM
	{
		public WorldVM(int myindex)
		{
			index = myindex;
			charname = null;

			connection = new MuckConnection();
			connection.EConnect += OnConnect;
			connection.ERead += OnRead;
			connection.ESend += OnSend;

			InitializeComponent();

			boxofinputboxes.Resize += OnInputBoxesResize;
			boxofinputboxes.EWantsToSend += OnInputBoxesHasTextToSend;
			OnInputBoxesResize(null, null);

			tbnRideMode.ESelect += OnRideModeSelected;

			Console = new DagMUWPF.Windows.Console() as IConsole;
			ElementHost.EnableModelessKeyboardInterop(Console as System.Windows.Window);
			Helpers.Add(Console);
			Console.Show();

			wf = new WF(this);

			logsettings = null;

			debugwindow = new DebugWindow();
			debugwindow.ESend += debugwindow_ESend;
			debugwindow.EStatusReset += debugwindow_EStatusReset;
			Helpers.Add(debugwindow);
	
			DescEditor = new DescEditorWindow();
			Helpers.Add(DescEditor);

			MorphHelper = new MorphHelperWindow();
			Helpers.Add(MorphHelper);

			WIHelper = new WIHelperWindow();
			Helpers.Add(WIHelper);

			FontsColors = new FontsColorsWindow();
			Helpers.Add(FontsColors);

			foreach (IHelper w in Helpers)
			{
				if (w is HelperForm) (w as HelperForm).parent = this;
				w.Hide();
			}

			boxofmucktext.BackColor = global::DagMU.Forms.Properties.Settings.Default.BoxForeColor;
			boxofmucktext.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::DagMU.Forms.Properties.Settings.Default, "BoxForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
	
			boxofmucktext.BackColor = global::DagMU.Forms.Properties.Settings.Default.BoxBackColor;
			boxofmucktext.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::DagMU.Forms.Properties.Settings.Default, "BoxBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			boxofmucktext.Font = global::DagMU.Forms.Properties.Settings.Default.Font;
			boxofmucktext.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DagMU.Forms.Properties.Settings.Default, "BoxFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
		}

		List<IHelper> Helpers = new List<IHelper>();

		List<CInfoHelperWindow> CInfoHelperWindows = new List<CInfoHelperWindow>();

		void OnInputBoxesHasTextToSend(object sender, string msg)
		{
			Send(msg, sender as InputBox);
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

		void OnConnect(object sender, MuckConnection.ConnectEventArgs args)
		{
			switch (args.Status) {
				case MuckConnection.ConnectEventArgs.StatusEnum.Connected:
					newStatus(MuckStatus.intercepting_connecting, args.Message);
					EConnected(this, null);
					break;

				case MuckConnection.ConnectEventArgs.StatusEnum.Got_Disconnected:
				case MuckConnection.ConnectEventArgs.StatusEnum.Error_Connecting:
					EDisconnected(this, null);
					newStatus(MuckStatus.not_connected, args.Message);
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
			Helpers.Add(cwin);
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
			Helpers.Remove((CInfoHelperWindow)sender);
			CInfoHelperWindows.Remove((CInfoHelperWindow)sender);
		}

		void OnRead(object sender, string text)
		{
			if (this.InvokeRequired) { this.Invoke((Action)(() => OnRead(sender, text))); return; }

			procline(text);
			Console.Print(text + '\n');
		}

		void OnSend(object inputbox, Tuple<MuckConnection.SendStatus, string> status_errarMessage)
		{
			if (this.InvokeRequired) {
				this.Invoke((Action)(() => OnSend(inputbox, status_errarMessage))); return;
			}

			switch (status_errarMessage.Item1)
			{
				case MuckConnection.SendStatus.send_error:
					boxprint("Send errar! " + status_errarMessage.Item2 ?? "");
					if (inputbox != null) (inputbox as InputBox).newstatus(InputBox.Status.Disconnected);
					Disconnect();
					break;

				case MuckConnection.SendStatus.sent:
					if (inputbox != null) (inputbox as InputBox).newstatus(InputBox.Status.Sent);
					break;
			}
		}

		public async Task Connect()
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
			EDisconnected(this, null);
			newStatus(MuckStatus.not_connected);
		}

		public void Send(String line, InputBox ib = null)
		{
			connection.Send(line, ib);
			Console.Print("]" + line + "\n");
		}

		public void SendFormat(String line, params object[] args)
		{
			Send(String.Format(line, args), null);
		}

		public void boxprint(String s)
		{
			if (boxofmucktext.InvokeRequired) { this.Invoke((Action)(() => boxprint(s))); return; }

			if (this.ParentForm == null) return;

			if (!this.ParentForm.Visible)
				User32.FlashWindow(this.ParentForm.Handle, true);

			Log(s);

			boxofmucktext.Add(s);
		}

		void newStatus(MuckStatus newstatus, string message = null)
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

		void Echo(string s, bool overrideEchoNotSet = false)
		{
			if (!echo && !overrideEchoNotSet) throw new InvalidOperationException("Echo not set.");
			SendFormat("{0} {1}{2} {3}",
				DagMU.Model.Constants.dagmu_echo_name,
				DagMU.Model.Constants.dagmu_echo_prefix,
				this.sessionGuid,
				s);
		}

		/// <param name="s">text from connection.</param>
		/// <param name="msg">Will be the contained message, if echo was valid.</param>
		/// <returns>True if valid.</returns>
		bool isEcho(string s, out string msg)
		{
			if (s.StartsWith(DagMU.Model.Constants.dagmu_echo_prefix)) {
				string remainder = s.Substring(DagMU.Model.Constants.dagmu_echo_prefix.Length);
				string guid = remainder.Substring(0, this.sessionGuid.Length);
				if (guid == this.sessionGuid) {
					msg = remainder.Substring(this.sessionGuid.Length).Trim();
					return true;
				}
			}

			msg = null;
			return false;
		}

		/// <param name="s">text from connection.</param>
		/// <param name="message">message to match against, if its a valid echo.</param>
		/// <returns>true if a valid echo matched supplied message.</returns>
		bool isEchoEqual(string s, string message)
		{
			string echoMessage;
			if (isEcho(s, out echoMessage)) {
				if (message == echoMessage) {
					this.echo = true;
					return true;
				}
			}
			return false;
		}

		void OnRideModeSelected(object sender, String ridemode)
		{
			Send("@set me=/ride/_mode:" + ridemode);//TAPS
			refocus();
		}
	}
}
