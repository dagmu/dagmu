using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading.Tasks;
using DagMU.Model;
using DagMU.Forms.Helpers;
using DagMUWPF.Windows;
using System.Windows.Forms.Integration;
using System.Collections.ObjectModel;

namespace DagMU.Forms
{
	partial class WorldVM : IRefocus
	{
		public WorldVM(int myindex)
		{
			Index = myindex;
			charName = null;
			Model.Data Data = new Model.Data();

			connection = new MuckConnection();
			connection.EConnect += onConnect;
			connection.ERead += onRead;
			connection.ESend += onSend;

			InitializeComponent();

			boxOfInputBoxes.Resize += onInputBoxesResize;
			boxOfInputBoxes.EScroll += boxOfInputBoxes_EScroll;
			boxOfInputBoxes.EWantsToSend += onInputBoxesHasTextToSend;
			onInputBoxesResize(null, null);

			tbnRideMode.ESelect += OnRideModeSelected;

			Console = new DagMUWPF.Windows.Console() as IConsole;
			ElementHost.EnableModelessKeyboardInterop(Console as System.Windows.Window);
			Helpers.Add(Console);
			Console.Show();

			wf = new WF(this);

			logSettings = null;

			debugWindow = new DebugWindow();
			debugWindow.ESend += debugWindow_ESend;
			debugWindow.EStatusReset += debugWindow_EStatusReset;
			Helpers.Add(debugWindow);
	
			DescEditor = new DescEditorWindow();
			Helpers.Add(DescEditor);

			MorphHelper = new MorphHelperWindow();
			Helpers.Add(MorphHelper);

			WIHelper = new WIHelperWindow();
			Helpers.Add(WIHelper);

			FontsColors = new FontsColorsWindow();
			Helpers.Add(FontsColors);

			Settings = new DagMUWPF.Windows.Settings() as IHelper;
			ElementHost.EnableModelessKeyboardInterop(Settings as System.Windows.Window);
			Helpers.Add(Settings);
			Settings.Show();

			foreach (IHelper w in Helpers)
			{
				if (w is HelperForm) (w as HelperForm).parent = this;
				w.Hide();
			}

			boxOfMuckText.BackColor = global::DagMU.Forms.Properties.Settings.Default.BoxForeColor;
			boxOfMuckText.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::DagMU.Forms.Properties.Settings.Default, "BoxForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
	
			boxOfMuckText.BackColor = global::DagMU.Forms.Properties.Settings.Default.BoxBackColor;
			boxOfMuckText.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::DagMU.Forms.Properties.Settings.Default, "BoxBackColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			boxOfMuckText.Font = global::DagMU.Forms.Properties.Settings.Default.Font;
			boxOfMuckText.DataBindings.Add(new System.Windows.Forms.Binding("Font", global::DagMU.Forms.Properties.Settings.Default, "BoxFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));

			boxOfMuckText.Refocus += (sender, args) => { boxOfInputBoxes.Refocus(); };
			boxOfMuckText.IoC(new Tuple<ObservableCollection<Data.TextMatch>, ObservableCollection<Data.TextMatch>>(Data.stuffToMatch, Data.namesToMatch));
		}

		List<IHelper> Helpers = new List<IHelper>();

		#region events
		public void onResizeBegin(object sender, System.EventArgs e)
		{
			boxOfMuckText.onResizeBegin(sender, e);
		}

		public void onResizeEnd(object sender, System.EventArgs e)
		{
			boxOfMuckText.onResizeEnd(sender, e);
		}

		void onInputBoxesHasTextToSend(object sender, string msg)
		{
			Send(msg, sender as InputBox);
		}

		/// <summary>
		/// Input box(es) have resized. Resize world window to fit.
		/// Triggers MainWindow resize event to resize actual window.
		/// </summary>
		/// <param name="sender">Unused.</param>
		/// <param name="e">Unused.</param>
		void onInputBoxesResize(object sender, EventArgs e)
		{
			SuspendLayout();

			boxOfMuckText.Anchor = AnchorStyles.None;
			boxOfMuckText.Top = menu.Bottom;

			boxOfInputBoxes.Top = boxOfMuckText.Bottom;// +Padding.Bottom;

			Height = menu.Height + boxOfMuckText.Height + boxOfInputBoxes.Height;// +Padding.Vertical + Padding.Vertical;

			boxOfMuckText.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

			ResumeLayout();
		}

		void onConnect(object sender, MuckConnection.ConnectEventArgs args)
		{
			switch (args.Status) {
				case MuckConnection.ConnectEventArgs.StatusEnum.Connected:
					NewStatus(MuckStatus.Intercepting_Connecting, args.Message);
					EConnected(this, null);
					Refocus();
					break;

				case MuckConnection.ConnectEventArgs.StatusEnum.Got_Disconnected:
				case MuckConnection.ConnectEventArgs.StatusEnum.Error_Connecting:
					EDisconnected(this, null);
					NewStatus(MuckStatus.NotConnected, args.Message);
					break;
			}
		}

		/// <summary>
		/// Received line from server.
		/// </summary>
		/// <param name="sender">Unused.</param>
		/// <param name="line">The line.</param>
		void onRead(object sender, string line)
		{
			if (this.InvokeRequired) { this.Invoke((Action)(() => onRead(sender, line))); return; }

			ProcLine(line);
			Console.Print(line + '\n');
		}

		void onSend(object inputbox, Tuple<MuckConnection.SendStatus, string> status_errarMessage)
		{
			if (this.InvokeRequired) {
				this.Invoke((Action)(() => onSend(inputbox, status_errarMessage))); return;
			}

			switch (status_errarMessage.Item1)
			{
				case MuckConnection.SendStatus.send_error:
					Errar("Send errar! " + status_errarMessage.Item2 ?? "");
					if (inputbox != null) (inputbox as InputBox).newstatus(InputBox.Status.Disconnected);
					Disconnect();
					break;

				case MuckConnection.SendStatus.sent:
					if (inputbox != null) (inputbox as InputBox).newstatus(InputBox.Status.Sent);
					break;
			}
		}

		/// <summary>
		/// Connected to server, but not character. Ready for input. Ready to send auto-connect.
		/// </summary>
		private void onReady()
		{
			if (prefs.sendOnConnect != null)
				Send(prefs.sendOnConnect);//send autoconnect
		}

		/// <summary>
		/// Connected to character, ready to sniff around.
		/// </summary>
		private void onLoggedIn()
		{
			Send("@mpi {name:me}");//get character name on connect!
			Send("exa me");
			Send("exa me=/_page/lastpaged");
			Send("exa me=/_whisp/lastwhispered");
			Send("exa me=/ride/_mode");

			Echo("LOGGEDIN", true);

			NewStatus(MuckStatus.Intercepting_Normal);// from here it is assumed login was successful

			Send("wf #hidefrom");
		}

		/// <summary>
		/// Connected, logged in, echo confirmed, ready to go into synced mode.
		/// </summary>
		private void onSynced()
		{
			Synced = true;

			boxOfMuckText.Clear();
			boxOfMuckText.ClearUndo();
			Send("look");//TAPS
		}

		private void OnRideModeSelected(object sender, String ridemode)
		{
			Send("@set me=/ride/_mode:" + ridemode);//TAPS
			Refocus();
		}
		#endregion

		#region cinfo
		List<CInfoHelperWindow> CInfoHelperWindows = new List<CInfoHelperWindow>();

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
		#endregion

		public async Task Connect()
		{
			if (connection.Connected)
				return;

			Print("Connecting to " + settings.NameFull + " [" + settings.Address + " : " + settings.Port + "]");

			try {
				await connection.Connect(settings.Address, settings.Port, settings.SSL, settings.CertificateHash);
			} catch (Exception e) {
				Errar("Error connecting: " + e.Message);
			}
		}

		public void Disconnect()
		{
			connection.Disconnect();
			EDisconnected(this, null);
			NewStatus(MuckStatus.NotConnected);
		}

		public void Send(String line, InputBox inputBox = null)
		{
			connection.Send(line, inputBox);
			Console.Print("]" + line + "\n");
		}

		public void Print(String s)
		{
			if (boxOfMuckText.InvokeRequired) { this.Invoke((Action)(() => Print(s))); return; }

			if (this.ParentForm == null) return;

			if (!this.ParentForm.Visible)
				User32.FlashWindow(this.ParentForm.Handle, true);

			Log(s);

			boxOfMuckText.Add(s);
		}

		public void Errar(string s)
		{
			Print("Errar: " + s);
		}

		void NewStatus(MuckStatus newStatus, string message = null)
		{
			//debugwindow.Text = ((int)newstatus).ToString();

			if (newStatus == MuckStatus.NotConnected) {
				Print(String.Join(": ", "Disconnected.", message));

				// tell input boxes to grey out or go away. grey out if there is text untyped, go away if they are empty
				boxOfInputBoxes.UpdateStatus(InputBox.Status.Disconnected);
			}

			status = newStatus;
			if (debugWindow != null)
				debugWindow.UpdateStatus(newStatus.ToString());
		}

		public void Refocus()
		{
			if (boxOfInputBoxes.InvokeRequired) { this.Invoke((Action)(() => Refocus())); return; }
			boxOfInputBoxes.Refocus();
		}

		void Echo(string s, bool overrideEchoNotSet = false)
		{
			if (!Synced && !overrideEchoNotSet) throw new InvalidOperationException("Echo not set.");
			Send(String.Format("{0} {1}{2} {3}",
				DagMU.Model.Constants.dagmu_echo_name,
				DagMU.Model.Constants.dagmu_echo_prefix,
				this.sessionGuid,
				s));
		}
	}
}
