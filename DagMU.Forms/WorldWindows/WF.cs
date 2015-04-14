using System;
using System.Windows.Forms;

namespace DagMU.Forms
{
	public partial class WF : UserControl
	{
		public WF(DagMU.Forms.World parentworld)
		{
			InitializeComponent(); 
			parent = parentworld;
			Hiding = HiddenEnum.Unknown;
			HiddenFor = 0;
		}

		private World parent;

		// Hidey stuff
		#region Hidey stuff
		private int hiddenfor_;
		/// <summary>
		/// Number of seconds the user has until they are automatically visible, if not normally hiding.
		/// This is the WF grace period they get when connecting. Range is between 0-60 seconds.
		/// Set to 1-60 seconds to start the timer. It will automatically stop at 0. Set to 0 to stop the timer early.
		/// </summary>
		public int HiddenFor // how many more seconds are we hidden, since we just came online?
		{
			get { return hiddenfor_; }
			set
			{
				if ((value < 0) || (value > 60))
					return;

				hiddenfor_ = value;

				if (value == 0)
				{
					labelVisibleIn.Visible = false;
					HiddenForTimer.Enabled = false;
				}
				else
				{
					if (!HiddenForTimer.Enabled)
						HiddenForTimer.Start();

					if (Hiding != HiddenEnum.HidingFromAll)
					{
						labelVisibleIn.Text = "Visible in " + hiddenfor_.ToString() + " seconds...";
						labelVisibleIn.Visible = true;
					}
					else
					{
						labelVisibleIn.Text = hiddenfor_.ToString() + "...";
						labelVisibleIn.Visible = true;
					}
				}
			}
		}
		private HiddenEnum hiding_; // This needs to be checked and set at SetNormal
		/// <summary>
		/// Is the user hiding from others' WFs? This is queried when we connect to the muck and automatically updated here.
		/// </summary>
		public HiddenEnum Hiding
		{
			get
			{
				return hiding_;
			}
			set
			{
				switch (value)
				{
					case HiddenEnum.Unknown:
						buttonHideFrom.Text = "Updating...";
						buttonHideFrom.Enabled = false;
						break;
					case HiddenEnum.HidingFromAll:
						// hiding from everyone
						buttonHideFrom.Text = "Hidden; Unhide";
						break;
					case HiddenEnum.VisibleToAll:
						// not hiding
						buttonHideFrom.Text = "Visible; Hide";
						break;
				}
				hiding_ = value;
			}
		}
		public enum HiddenEnum
		{
			HidingFromAll,
			VisibleToAll,
			Unknown
		}
		private void HiddenForTimer_Tick(object sender, EventArgs e)
		{
			if (Hiding != HiddenEnum.HidingFromAll)
			{
				labelVisibleIn.Text = "Visible in " + hiddenfor_.ToString() + " seconds...";
				labelVisibleIn.Visible = true;
			}
			else
			{
				labelVisibleIn.Text = hiddenfor_.ToString() + "...";
				labelVisibleIn.Visible = true;
			}
		}
		#endregion

		// These are called by World when the WF status changes

		// Fullupdate is the response we get when we type WF
		// This information is always reliable
		#region Handle full WF updates
		/// <summary>
		/// No one that you are watching for is online.
		/// </summary>
		public void UpdateFullNoone()
		{
		}

		/// <summary>
		/// WF starting, clear temp list
		/// </summary>
		public void UpdateFullStarting()
		{
		}

		/// <summary>
		/// add a name to our temp list
		/// </summary>
		/// <param name="name">name to add</param>
		public void UpdateFullNames(String name)
		{
		}

		/// <summary>
		/// WF done, compare temp list to current list
		/// </summary>
		public void UpdateFullDone()
		{
		}

		/// <summary>
		/// If we get the 'players online for whom you are watching:' and then something else, we abort
		/// </summary>
		public void UpdateFullAbort()
		{
		}
		#endregion

		// These are on-the-fly messages from the muck that someone maybe has dis/connected
		// These updates are from "Somewhere on the muck"s and are sometimes unreliable
		#region Handle on the fly updates from the muck
		/// <summary>
		/// Someone has connected to the muck, put them on the list if not already
		/// </summary>
		/// <param name="name">Who has connected</param>
		public void UpdateOneConnected(String name)
		{
		}

		/// <summary>
		/// Someone has connected to the muck, put them on the list if not already
		/// </summary>
		/// <param name="name">Who has connected</param>
		/// <param name="silent">Whether to indicate visually (flash or whatever) that a name has connected</param>
		public void UpdateOneConnected(String name, bool silent)
		{
		}

		/// <summary>
		/// Someone has disconnected from the muck, take them off the WF list
		/// </summary>
		/// <param name="name">Name to remove, will be searched</param>
		public void UpdateOneDisconnected(String name)
		{
		}

		/// <summary>
		/// Removes an actual listview item from the list, called by UpdateOneDisconnected(String name)
		/// </summary>
		/// <param name="item">Entry to remove</param>
		public void UpdateOneDisconnected(ListViewItem item)
		{
		}

		#endregion
	}
}
