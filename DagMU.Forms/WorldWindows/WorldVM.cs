using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

using DagMU.Forms.Helpers;
using DagMU.Model;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DagMU.Forms
{
	public partial class WorldVM : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// World Settings - these don't change except if the muck changes its name or address or how it displays text.
		/// </summary>
		public class WorldSettings
		{
			public String NameFull;			// the muck's full name
			public String NameShort;		// the muck's shorter name, for the titlebar and menus

			public String Address;			// name or ip of muck
			public int Port;				// port number
			public bool SSL;				// use SSL or not?
			public string CertificateHash;	// remote server certificate hash

			public int sendBufferSize;		// length of text we can send to the muck, this is only used locally to alert the user

			public String sendQUITstring;	// string to send for QUIT, i.e. "QUIT"
		} public WorldSettings settings;

		/// <summary>
		/// Personal world settings - user preferences to react to and display the world
		/// </summary>
		public class WorldPrefs
		{
			public String sendOnConnect;	// (character login string)
		} public WorldPrefs prefs;

		// Current Status
		/// Muck communication status indicators. These change at runtime and not saved
		public enum MuckStatus
		{
			NotConnected,
			Intercepting_Connecting,	// socket established, receiving welcome screen
			Intercepting_Connecting2,	// logged in, receive login success or failure message
			Intercepting_Normal,		// >>> normal user interaction mode <<<
			Intercepting_WF,			// receiving WF list
			Intercepting_WS,			// receiving WS list
			Intercepting_WI,			// receiving WI list
			Intercepting_WIFlags,		// wi #flags
			Intercepting_Who,			// receiving WHO list (the big one)
			Intercepting_Last,			// Waiting for the rest of the laston report (to detect hidden furs to put on our wf list?)
			Intercepting_Morph,			// receiving 'morph #list'
			Intercepting_CInfo,			// receiving cinfo fields
			Intercepting_CInfoMisc,		// snagging a single misc field
			Intercepting_CharName,		// reading our own character name
			Intercepting_ExaMe,			// exa me, self properties, desc setting
		} public MuckStatus status;

		String charName;
		public String CharName
		{
			get { return charName; }
			set { charName = value; debugWindow.textboxCharName.Text = value; }
		}

		string lastWhispered;	// these track the last person you paged/whispered to,
		string lastPaged;		// so when you type "p =" it will fill in the name as soon as you type the = sign
		bool inLimbo;
		bool echo = false;		// is dagmuecho working?

		bool expectingDescSettingLine = false; // for catching description in exame
		bool expectingActionsExits = false; // for detecting the end of exame block

		List<string> characterDescLists = new List<string>();//redesc, desc2.. any {list:desc} that was found in desc string, scraped from 'exa me'

		string sessionGuid = DagMU.Model.Utils.GuidEncoder.Encode(Guid.NewGuid());

		MuckConnection connection;

		int morphsIntercepted;
		string cInfoInterceptingName;
		string cInfoInterceptingFieldName;

		// Components
		Box boxOfMuckText;
		InputBoxBox boxOfInputBoxes;
		ToolStrip menu;

		WF wf;
		DebugWindow debugWindow;
		LogSettingsWindow logSettings;

		internal IConsole Console;
		internal Helpers.DescEditorWindow DescEditor;
		internal Helpers.MorphHelperWindow MorphHelper;
		internal Helpers.FontsColorsWindow FontsColors;
		internal Helpers.WIHelperWindow WIHelper;
		internal Helpers.WhoHelperWindow Who;

		Helpers.RideModeDropdown tbnRideMode;
		System.Windows.Forms.ToolStripButton tbnDisconnect;
		System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		System.Windows.Forms.ToolStripDropDownButton tbnSettingsDropDown;
		System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		System.Windows.Forms.ToolStripSplitButton tbnLog;
		System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		System.Windows.Forms.ToolStripMenuItem tbnLogSettings;
		System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		System.Windows.Forms.ToolStripButton tbnDesc;
		System.Windows.Forms.ToolStripButton tbnCInfo;
		System.Windows.Forms.ToolStripButton tbnWI;
		System.Windows.Forms.ToolStripButton tbnWF;
		System.Windows.Forms.ToolStripButton toolStripButtonConsole;
		System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		System.Windows.Forms.ToolStripButton tbnFonts;
		System.Windows.Forms.ToolStripTextBox tbnLogName;
		System.Windows.Forms.ToolStripButton tbnMorphs;
		System.Windows.Forms.ToolStripButton tbnDebug;

		void InitializeComponent()
		{
			this.menu = new ToolStrip();
			this.tbnDisconnect = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnFonts = new System.Windows.Forms.ToolStripButton();
			this.tbnSettingsDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnLog = new System.Windows.Forms.ToolStripSplitButton();
			this.tbnLogName = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnLogSettings = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnDesc = new System.Windows.Forms.ToolStripButton();
			this.tbnMorphs = new System.Windows.Forms.ToolStripButton();
			this.tbnCInfo = new System.Windows.Forms.ToolStripButton();
			this.tbnWI = new System.Windows.Forms.ToolStripButton();
			this.tbnWF = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnRideMode = new DagMU.Forms.Helpers.RideModeDropdown();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tbnDebug = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonConsole = new System.Windows.Forms.ToolStripButton();
			this.boxOfInputBoxes = new DagMU.Forms.Helpers.InputBoxBox();
			this.boxOfMuckText = new DagMU.Forms.Box();
			this.menu.SuspendLayout();
			this.SuspendLayout();
			// 
			// menu
			// 
			this.menu.CanOverflow = false;
			this.menu.Dock = System.Windows.Forms.DockStyle.None;
			this.menu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbnDisconnect,
            this.toolStripSeparator1,
            this.tbnFonts,
            this.tbnSettingsDropDown,
            this.tbnLog,
            this.toolStripSeparator2,
            this.tbnDesc,
            this.tbnMorphs,
            this.tbnCInfo,
            this.tbnWI,
            this.tbnWF,
            this.toolStripSeparator4,
            this.tbnRideMode,
            this.toolStripSeparator3,
            this.tbnDebug,
            this.toolStripButtonConsole});
			this.menu.Location = new System.Drawing.Point(36, 0);
			this.menu.Name = "menu";
			this.menu.Size = new System.Drawing.Size(460, 25);
			this.menu.TabIndex = 5;
			this.menu.Text = "glassToolStrip1";
			// 
			// tbnDisconnect
			// 
			this.tbnDisconnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbnDisconnect.Image = global::DagMU.Forms.Properties.Resources.lightning_delete;
			this.tbnDisconnect.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnDisconnect.Name = "tbnDisconnect";
			this.tbnDisconnect.Size = new System.Drawing.Size(23, 22);
			this.tbnDisconnect.Text = "Disconnect";
			this.tbnDisconnect.Click += new System.EventHandler(this.tbnDisconnect_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tbnFonts
			// 
			this.tbnFonts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbnFonts.Image = global::DagMU.Forms.Properties.Resources.font;
			this.tbnFonts.Name = "tbnFonts";
			this.tbnFonts.Size = new System.Drawing.Size(23, 22);
			this.tbnFonts.Text = "Fonts and Colors";
			this.tbnFonts.Click += new System.EventHandler(this.tbnFonts_Click);
			// 
			// tbnSettingsDropDown
			// 
			this.tbnSettingsDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbnSettingsDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5});
			this.tbnSettingsDropDown.Enabled = false;
			this.tbnSettingsDropDown.Image = global::DagMU.Forms.Properties.Resources.cog_go;
			this.tbnSettingsDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnSettingsDropDown.Name = "tbnSettingsDropDown";
			this.tbnSettingsDropDown.Size = new System.Drawing.Size(29, 22);
			this.tbnSettingsDropDown.Text = "Settings";
			this.tbnSettingsDropDown.Click += new System.EventHandler(this.tbnSettingsDropDown_Click);
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(57, 6);
			// 
			// tbnLog
			// 
			this.tbnLog.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbnLog.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbnLogName,
            this.toolStripSeparator6,
            this.tbnLogSettings});
			this.tbnLog.Image = global::DagMU.Forms.Properties.Resources.pencil;
			this.tbnLog.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnLog.Name = "tbnLog";
			this.tbnLog.Size = new System.Drawing.Size(32, 22);
			this.tbnLog.Text = "Logging";
			this.tbnLog.ButtonClick += new System.EventHandler(this.tbnLogging_ButtonClick);
			this.tbnLog.MouseEnter += new System.EventHandler(this.tbnLog_MouseEnter);
			this.tbnLog.MouseLeave += new System.EventHandler(this.tbnLog_MouseLeave);
			// 
			// tbnLogName
			// 
			this.tbnLogName.Enabled = false;
			this.tbnLogName.Name = "tbnLogName";
			this.tbnLogName.Size = new System.Drawing.Size(183, 23);
			this.tbnLogName.Leave += new System.EventHandler(this.tbnLogName_Leave);
			this.tbnLogName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbnLogName_KeyDown);
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(240, 6);
			// 
			// tbnLogSettings
			// 
			this.tbnLogSettings.Name = "tbnLogSettings";
			this.tbnLogSettings.Size = new System.Drawing.Size(243, 22);
			this.tbnLogSettings.Text = "Logging Settings";
			this.tbnLogSettings.Click += new System.EventHandler(this.tbnLogSettings_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tbnDesc
			// 
			this.tbnDesc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tbnDesc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbnDesc.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnDesc.Name = "tbnDesc";
			this.tbnDesc.Size = new System.Drawing.Size(40, 22);
			this.tbnDesc.Text = "Desc";
			this.tbnDesc.Click += new System.EventHandler(this.tbnDesc_Click);
			// 
			// tbnMorphs
			// 
			this.tbnMorphs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tbnMorphs.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbnMorphs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnMorphs.Name = "tbnMorphs";
			this.tbnMorphs.Size = new System.Drawing.Size(58, 22);
			this.tbnMorphs.Text = "Morphs";
			this.tbnMorphs.Click += new System.EventHandler(this.tbnMorphs_Click);
			// 
			// tbnCInfo
			// 
			this.tbnCInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tbnCInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.tbnCInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnCInfo.Name = "tbnCInfo";
			this.tbnCInfo.Size = new System.Drawing.Size(42, 22);
			this.tbnCInfo.Text = "CInfo";
			this.tbnCInfo.Click += new System.EventHandler(this.tbnCInfo_Click);
			// 
			// tbnWI
			// 
			this.tbnWI.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tbnWI.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnWI.Name = "tbnWI";
			this.tbnWI.Size = new System.Drawing.Size(25, 22);
			this.tbnWI.Text = "WI";
			this.tbnWI.ToolTipText = "WhatIs Helper";
			this.tbnWI.Click += new System.EventHandler(this.tbnWI_Click);
			// 
			// tbnWF
			// 
			this.tbnWF.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tbnWF.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnWF.Name = "tbnWF";
			this.tbnWF.Size = new System.Drawing.Size(38, 22);
			this.tbnWF.Text = "-WF-";
			this.tbnWF.Click += new System.EventHandler(this.tbnWF_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tbnRideMode
			// 
			this.tbnRideMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.tbnRideMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.tbnRideMode.Items.AddRange(new object[] {
            "ride",
            "hand",
            "walk",
            "fly"});
			this.tbnRideMode.Name = "tbnRideMode";
			this.tbnRideMode.Size = new System.Drawing.Size(75, 25);
			this.tbnRideMode.ToolTipText = "Current Ride Mode";
			this.tbnRideMode.ESelect += tbnRideMode_ESelect;
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tbnDebug
			// 
			this.tbnDebug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tbnDebug.Image = global::DagMU.Forms.Properties.Resources.cog_error;
			this.tbnDebug.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tbnDebug.Name = "tbnDebug";
			this.tbnDebug.Size = new System.Drawing.Size(23, 22);
			this.tbnDebug.Text = "Debug Window";
			this.tbnDebug.Click += new System.EventHandler(this.tbnDebug_Click);
			// 
			// toolStripButtonConsole
			// 
			this.toolStripButtonConsole.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonConsole.Image = global::DagMU.Forms.Properties.Resources.application_xp_terminal;
			this.toolStripButtonConsole.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonConsole.Name = "toolStripButtonConsole";
			this.toolStripButtonConsole.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonConsole.Text = "Console Window";
			this.toolStripButtonConsole.Click += new System.EventHandler(this.toolStripButtonConsole_Click);
			// 
			// boxofinputboxes
			// 
			this.boxOfInputBoxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.boxOfInputBoxes.BackColor = System.Drawing.Color.Navy;
			this.boxOfInputBoxes.Location = new System.Drawing.Point(0, 449);
			this.boxOfInputBoxes.Margin = new System.Windows.Forms.Padding(0);
			this.boxOfInputBoxes.Name = "boxofinputboxes";
			this.boxOfInputBoxes.Size = new System.Drawing.Size(773, 57);
			this.boxOfInputBoxes.TabIndex = 1;
			this.boxOfInputBoxes.EScroll += boxOfInputBoxes_EScroll;
			// 
			// boxofmucktext
			// 
			this.boxOfMuckText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.boxOfMuckText.AutoWordSelection = true;
			this.boxOfMuckText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.boxOfMuckText.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.boxOfMuckText.Cursor = System.Windows.Forms.Cursors.Default;
			this.boxOfMuckText.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.boxOfMuckText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.boxOfMuckText.Location = new System.Drawing.Point(0, 0);
			this.boxOfMuckText.Margin = new System.Windows.Forms.Padding(0);
			this.boxOfMuckText.Name = "boxofmucktext";
			this.boxOfMuckText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.boxOfMuckText.Size = new System.Drawing.Size(773, 449);
			this.boxOfMuckText.TabIndex = 0;
			this.boxOfMuckText.Text = "";
			this.boxOfMuckText.LinkClicked += boxOfMuckText_LinkClicked;
			this.boxOfMuckText.Refocus += boxOfMuckText_Refocus;
			// 
			// World
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.menu);
			this.Controls.Add(this.boxOfInputBoxes);
			this.Controls.Add(this.boxOfMuckText);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "World";
			this.Size = new System.Drawing.Size(773, 506);
			this.menu.ResumeLayout(false);
			this.menu.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		private void boxOfMuckText_Refocus(object sender, EventArgs e)
		{
			boxOfInputBoxes.Refocus();
		}

		public bool Connected {
			get {
				return connection.Connected;
			}
		}

		void boxOfInputBoxes_EScroll(object sender, MouseEventArgs e)
		{
			User32.SendMouseWheelEvent(boxOfMuckText, e.Delta);
		}

		void HelperShow(IHelper hw)
		{
			hw.Show();
		}

		void tbnDisconnect_Click(object sender, EventArgs e)
		{
			if (connection.Connected)
				Disconnect();

			EClosing(this, null);
		}

		void tbnWF_Click(object sender, EventArgs e)
		{
			wf.Show();
			wf.BringToFront();
		}

		void tbnDesc_Click(object sender, EventArgs e)
		{
			HelperShow(DescEditor);
			DescEditor.DescMode = true;
		}

		void tbnFonts_Click(object sender, EventArgs e)
		{
			HelperShow(FontsColors);
		}

		void tbnWI_Click(object sender, EventArgs e)
		{
			HelperShow(WIHelper);
		}

		void tbnMorphs_Click(object sender, EventArgs e)
		{
			HelperShow(MorphHelper);
		}

		void tbnCInfo_Click(object sender, EventArgs e)
		{
			if (CharName == null)
				return;

			CInfoHelperWindow cwin = FindCInfoWindow(CharName);
			if (cwin == null) {
				Send("cinfo " + CharName, null);
				cwin = MakeCInfoWindow(CharName);
			}

			cwin.Show();
			cwin.BringToFront();
		}

		void toolStripButtonConsole_Click(object sender, EventArgs e)
		{
			HelperShow(Console);
		}

		void tbnSettingsDropDown_Click(object sender, EventArgs e)
		{
		}

		void tbnDebug_Click(object sender, EventArgs e)
		{
			debugWindow.Show();
			debugWindow.BringToFront();
		}

		void debugWindow_EStatusReset(object sender, EventArgs e)
		{
			newStatus(MuckStatus.Intercepting_Normal);
		}

		void debugWindow_ESend(object sender, string s)
		{
			ProcLine(s);
		}

		void tbnLogSettings_Click(object sender, EventArgs e)
		{
			if (logSettings == null)
				logSettings = new LogSettingsWindow();

			logSettings.FormClosing += logSettings_FormClosing;
			logSettings.Show();
			logSettings.BringToFront();
		}

		void logSettings_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			logSettings = null;
		}

		public void reFocus()
		{
			boxOfInputBoxes.Refocus();
		}

		/// <summary>
		/// This world's ID number in the currentcollection of worlds
		/// </summary>
		public int Index { get { return index; } }
		int index;

		public event EventHandler EConnected;
		public event EventHandler EDisconnected;
		public event EventHandler EClosing;

		#region logging
		public bool Logging
		{
			get { return (LoggingTo != null); }
			set {
				if (value) LoggingQuickStart();
				else LoggingStop();
			}
		}

		String loggingTo;
		StreamWriter LoggingStream;

		public String LoggingTo
		{
			get
			{
				return loggingTo;
			}
			set
			{
				if (value == loggingTo)
					return;

				if (Logging)
				{
					// stop logging
					String loggingToSave = loggingTo;
					LoggingStop();
					loggingTo = loggingToSave;
					// can't File.Move on network paths, copy+delete instead
					System.IO.File.Copy(LoggingPath + loggingTo + LoggingExtension, LoggingPath + value + LoggingExtension, true);
					System.IO.File.Delete(LoggingPath + loggingTo + LoggingExtension);
					// restart at new place, omit history
					LoggingStart(value, false);
				}
				else
				{
					LoggingStart(value, true);
				}
				loggingTo = value;
				SuspendLayout();
				tbnLogName.Text = value;
				tbnLogName.Enabled = true;
				ResumeLayout();
			}
		}

		String LoggingPath
		{
			get
			{
				return Properties.Settings.Default.LogDir + System.IO.Path.DirectorySeparatorChar;
			}
		}

		String LoggingExtension
		{
			get
			{
				return ".txt";
			}
		}

		String LogDate(DateTime now)
		{
			return now.ToString("yyyy.MM.dd");
		}

		void LoggingQuickStart()
		{
			DateTime offsetday = DateTime.Now.AddHours(-1 * (double)Properties.Settings.Default.LogOffset);
			String plusstring = String.Empty;
			String filename, path;
			int padwidth = 0;

			do
			{
				plusstring = plusstring.PadRight(padwidth, '_');
				filename = LogDate(offsetday) + " " + CharName + plusstring;
				padwidth++;
				path = LoggingPath + filename + LoggingExtension;
			}
			while (System.IO.File.Exists(path));

			// now we have a filename that will work
			LoggingStart(filename, true);
		}

		void LoggingSaveHistory()
		{
			boxOfMuckText.SaveFile(LoggingStream.BaseStream, System.Windows.Forms.RichTextBoxStreamType.PlainText);
		}

		void LoggingStart(String logfilename, bool includehistory)
		{
			loggingTo = logfilename;
			String logpath = LoggingPath + LoggingTo + LoggingExtension;
			String firstline = "Log file for " + CharName + ", " + LogDate(System.DateTime.Now) + " " + System.DateTime.Today.ToShortTimeString() + Environment.NewLine;
			LoggingStream = System.IO.File.AppendText(logpath);
			LoggingStream.AutoFlush = true;
			if (includehistory)
			{
				LoggingStream.WriteLine(firstline);
				LoggingSaveHistory();
			}
			SuspendLayout();
			tbnLogName.Text = LoggingTo;
			tbnLogName.Enabled = true;
			ResumeLayout();
		}

		void LoggingStop()
		{
			if (!Logging)
				return;
			loggingTo = null;
			LoggingStream.Close();
			tbnLogName.Clear();
			tbnLogName.Enabled = false;
		}

		void Log(String s)
		{
			if (!Logging) return;
			LoggingStream.WriteLine(s + Environment.NewLine);
		}

		void tbnLogging_ButtonClick(object sender, EventArgs e)
		{
			if (Logging)
				LoggingStop();
			else
				LoggingQuickStart();
		}

		void tbnLog_MouseEnter(object sender, EventArgs e)
		{
			if (Logging)
			{
				tbnLog.ToolTipText = "Stop logging.";
				tbnLog.Image = ((System.Drawing.Image)(Properties.Resources.pencil_delete));
			}
			else
			{
				tbnLog.ToolTipText = "Start logging";
				tbnLog.Image = ((System.Drawing.Image)(Properties.Resources.pencil_add));
			}
		}

		void tbnLog_MouseLeave(object sender, EventArgs e)
		{
			if (Logging)
				tbnLog.Image = ((Image)(Properties.Resources.pencil_go));
			else
				tbnLog.Image = ((Image)(Properties.Resources.pencil));
		}

		void tbnLogName_Leave(object sender, EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(LoggingTo + " " + tbnLogName.Text);
			LoggingTo = tbnLogName.Text;
		}

		void tbnLogName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Enter)
			{
				LoggingTo = tbnLogName.Text;
				e.SuppressKeyPress = true;
			}
		}
#endregion

		void tbnRideMode_ESelect(object sender, string s)
		{
			Send("@set me=/ride/_mode:" + s, null);//TAPS
		}

		void boxOfMuckText_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.LinkText);
		}
	}
}
