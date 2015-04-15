using System;
using System.ComponentModel;
using System.Linq;

namespace DagMU.Forms.HelperWindows
{
	public partial class DescEditorWindow : DagMU.Forms.HelperWindows.HelperForm
	{
		public DescEditorWindow()
		{
			InitializeComponent();
		}

		bool descmode;
		public bool DescMode
		{
			get { return descmode; }
			set
			{
				// switch between desc editor and morph editor

				// if not changing, don't bother
				if (descmode == value)
					return;

				if (descmode)
				{
					// turning into morph mode
					muckActionText.Enabled = true;
					muckLongName.Enabled = true;
					label3.Text = "morph name:";
					buttonMorph.Enabled = true;
					buttonQMorph.Enabled = true;
					foreach (MuckPropertyTextBox p in Controls.OfType<MuckPropertyTextBox>())
						p.Input = "";
				}
				else
				{
					// turning into desc mode
					muckActionText.Enabled = false;
					muckLongName.Enabled = false;
					label3.Text = "name:";
					buttonMorph.Enabled = false;
					buttonQMorph.Enabled = false;
					foreach (MuckPropertyTextBox p in Controls.OfType<MuckPropertyTextBox>())
						p.Input = "";
				}

				descmode = value;
			}
		}

		void buttonRefresh_Click(object sender, EventArgs e)
		{
			if (descmode)
			{
				foreach (MuckPropertyTextBox p in Controls.OfType<MuckPropertyTextBox>())
				{
					String s = p.MuckCommandToGet;
					p.Enabled = false;
					if (s != null)
						parent.Send(s, null);
				}
				return;
			}
			else
			{
				// morph mode skips normal muckcommands embedded in controls because morph name has to be inserted in the commands

				foreach (MuckPropertyTextBox p in Controls.OfType<MuckPropertyTextBox>())
					p.Enabled = false;

				if (labelName.Text.Length == 0)
					Close();

				parent.Send("exa me=/morph#/" + labelName.Text + "#/", null);
				parent.Send("exa me=/morph#/" + labelName.Text + "#/desc#/", null);
			}
		}

		void buttonSave_Click(object sender, EventArgs e)
		{
			if (descmode)
			{
				foreach (MuckPropertyTextBox p in Controls.OfType<MuckPropertyTextBox>())
				{
					String s = p.MuckCommandToSet;

					if (!p.Checked)
						continue;

					if (s != null)
						parent.Send(s + p.Input, null);
					else
					{
						parent.Send("lsedit me=redesc", null);
						parent.Send(".del 1 1000", null);

						String[] delims = { "\r\n" };
						String[] lines = p.Input.Split(delims, StringSplitOptions.RemoveEmptyEntries);
						foreach (String line in lines)
						{
							parent.Send(line, null);
						}
						parent.Send(".end", null);
					}

					p.Checked = false;
				}
			}
			else
			{
				// morph editing

				//str /morph#/boxers#/desc#/:7
				//str /morph#/boxers#/message:changes clothes!
				//str /morph#/boxers#/name:anthro boxer shorts nothin else
				//str /morph#/boxers#/osay:rumbles ~%m~
				//str /morph#/boxers#/say:You rumble ~%m~
				//str /morph#/boxers#/scent:The wolf smells like wolf, that's for certain. He may be of mixed heritidge, but he's all canine, and his lupine side has taken over his scent. His fur is clean though, at least within a few days.
				//str /morph#/boxers#/sex:male
				//str /morph#/boxers#/species:anthro dark wolf

				if (muckLongName.Checked) {
					// send set morph name
					parent.Send("@set me=/morph#/" + labelName.Text + "#/name:" + muckLongName.Input, null);
					muckLongName.Checked = false;
				}
				if (muckActionText.Checked) {
					// set morph message
					parent.Send("@set me=/morph#/" + labelName.Text + "#/message:" + muckActionText.Text, null);
					muckActionText.Checked = false;
				}
				if (muckGender.Checked) {
					//send set gender command to muck connection
					parent.Send("@set me=/morph#/" + labelName.Text + "#/sex:" + muckGender.Input, null);
					muckGender.Checked = false;
				}
				if (muckSpecies.Checked) {
					//send set species command to muck connection
					parent.Send("@set me=/morph#/" + labelName.Text + "#/species:" + muckSpecies.Input, null);
					muckSpecies.Checked = false;
				}
				if (muckScent.Checked) {
					//send scent command to muck connection
					parent.Send("@set me=/morph#/" + labelName.Text + "#/scent:" + muckScent.Text, null);
					muckScent.Checked = false;
				}
				if (muckSay.Checked)
				{
					//send set what you see when you speak
					parent.Send("@set me=/morph#/" + labelName.Text + "#/say:" + muckSay.Input, null);
					muckSay.Checked = false;
				}
				if (muckOSay.Checked)
				{
					//send set what others see when you speak
					parent.Send("@set me=/morph#/" + labelName.Text + "#/osay:" + muckOSay.Input, null);
					muckOSay.Checked = false;
				}
				if (muckDesc.Checked) {
					//send set desc command to muck connection

					parent.Send("lsedit me=/morph#/" + labelName.Text + "#/desc", null);
					parent.Send(".del 1 1000", null);

					String[] delims = {"\r\n"};
					String[] strings = muckDesc.Input.Split(delims,StringSplitOptions.RemoveEmptyEntries);
					foreach (String s in strings) {
						parent.Send(s, null);
					}
					parent.Send(".end", null);

					muckDesc.Checked = false;
				}
			}
		}

		public void updateGender(String s)
		{
			if (!descmode)
				checkCommandName(labelName.Text);
			muckGender.GotUpdate(s);
		}
		public void updateSpecies(String s)
		{
			if (!descmode)
				checkCommandName(labelName.Text);
			muckSpecies.GotUpdate(s);
		}
		public void updateScent(String s)
		{
			if (!descmode)
				checkCommandName(labelName.Text);
			muckScent.GotUpdate(s);
		}
		public void updateDesc(String s, int linenum)
		{
			if (!descmode)
				checkCommandName(labelName.Text);
			muckDesc.GotUpdate(s, linenum);
		}
		public void updateSay(String s)
		{
			if (!descmode)
				checkCommandName(labelName.Text);
			muckSay.GotUpdate(s);
		}
		public void updateOSay(String s)
		{
			muckOSay.GotUpdate(s);
		}
		public void updateCommand(String s)
		{
			checkCommandName(s);
		}
		public void updateName(String s)
		{
			checkCommandName(labelName.Text);
			muckLongName.GotUpdate(s);
		}
		public void updateMessage(String s)
		{
			checkCommandName(labelName.Text);
			muckActionText.GotUpdate(s);
		}

		void buttonTest_Click(object sender, EventArgs e)
		{
			updateGender("male");
			updateSpecies("rar");
			updateScent("Good");
			updateDesc("rarrarr", 1);
			updateSay("");
			updateOSay("");
		}

		public void checkCommandName(string target)
		{
			// when updating info, we first check to see if we're updating the morph that's currently in the editor
			// if we're updating a new morph, clear everything out of the editor to start new with the new data

			// if the morph is different that what's in, clear the whole form in anticipation of an update coming soon

			DescMode = false;
			
			if (labelName.Text != target)
			{
				foreach (MuckPropertyTextBox p in Controls.OfType<MuckPropertyTextBox>())
				{
					p.Input = "";
					p.Enabled = false;
				}
				labelName.Text = target;
			}
		}

		void DescEditorWindow_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			if (descmode)
			System.Windows.Forms.MessageBox.Show(
				"This window changes your CURRENT muck appearance, and it will not affect any morphs. "+
				"To save your changes for later quick access, you should use morphs.\n\n"+

				"Tips:\n"+
				"Don't use multiple lines for scent.\n"+
				"WS will only show the first nine characters of your sex.\n"+
				"Let the desc wordwrap, don't break paragraphs into multiple lines.\n\n"+

				"This will not magically update if you change your appearance by any means outside of this window. If so, clicking refresh is all it takes to make this window accurate again.\n\n"+

				"There will be no muck feedback to tell you this worked, but it will! Check the console if you don't believe me!\n\n"+

				"How this window works, behind the scenes:\n"+
				"DagMU intercepts any printout of /redesc#/, /_scent, /species, /sex, /_say/format, and /_say/oformat\n"+
				"When you click apply, any checked items are sent to the muck via '@set me=/_scent:Awful.' etc. lsedit is used to set the \"redesc\" list, which most people have as their current desc. "+
				"Output from these properties are still visible in the Console window, if you really want to see the raw data outside of the GUI.",
				"Appearance Editor Help"
				);
			else
				System.Windows.Forms.MessageBox.Show(
				"This is the morph editor! " +
				"This may be a bit of overkill functionality, but I hope you find it useful.\n\n" +

				"This allows you to edit a morph's information without having to put it on, just as if you were simply editing a text file. Note that if you edit the morph you are currently wearing it will not change your current appearance until you morph into it again.\n\n" +

				"Click 'morph' or 'qmorph' to put this morph on now. If you've made changes, save them first!\n\n" +

				"The 'long name' is for your own reference, it is what will appear in the Morph Helper's list.\n\n" +

				"Action text is an :emote that will display to the muck when you morph. A prefixed : is not needed. The action text will be a default message if left blank, and will be silent if you use 'quiet' morphing.\n\n" +

				"How this works, behind the scenes:\n" +
				"DagMU intercepts everything in the /morph#/ folder and populates this window with whatever it sees last. Clicking apply sets the properties using '@set me=/morph#/____#/species:gay wolf' etc. Desc is set via 'lsedit me=/morph#/____#/desc'.",
				"Morph Editor Help"
			);
			e.Cancel = true;
		}

		void buttonMorph_Click(object sender, EventArgs e)
		{
			if (labelName.Text.Length > 0)
				parent.Send("morph " + labelName.Text, null);
		}

		void buttonQMorph_Click(object sender, EventArgs e)
		{
			if (labelName.Text.Length > 0)
				parent.Send("qmorph " + labelName.Text, null);

		}

		void buttonMorphs_Click(object sender, EventArgs e)
		{

		}
	}
}
