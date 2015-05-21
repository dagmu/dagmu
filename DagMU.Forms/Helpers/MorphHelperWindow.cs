using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace DagMU.Forms.Helpers
{
	public partial class MorphHelperWindow : DagMU.Forms.Helpers.HelperForm
	{
		public MorphHelperWindow()
		{
			moremore = true;
			InitializeComponent();
			togglewindow();
		}

		public void updateMorphs(String input)
		{
			// i'm not sure this is needed, we get much better results via 'morph #list'

			//  exa me=/_regmorphs
			//str /_regmorphs:  TIGER    DRAGON    WOLF    ELF    TEACHER          PURPLE          PANTSDRGN      SWIMDRGN    TEACHDRGN    DRGN    PANTSTGR    TGR    BUFFTGR    TEACHTGR    LABTGR    gothwolf    weredrgn    xmasptgr    minidrag    robedrgn    invis    mdragon    hallodrgn    sldrtgr    47drgn  

			// we receive a weirdly space delimited list of morph COMMAND names, use this master list to populate our listbox
			String[] delimspace = {" "};
			String[] words = input.Split(delimspace,StringSplitOptions.RemoveEmptyEntries);

			// add morphs to listbox if they're not already there
			foreach (String w in words) {
				// if !findinlistbox add to listbox
				ifatl(w);
			}

			// reverse check: remove morphs from listbox that are no longer on master list from muck
			// for each commandname in listbox
				// if !findinINPUT
				// remove listbox entry
		}

		public void updateMorphName(string commandname, string fullname)
		{
			// update the 'full name' of the morph, which is me=/morph#/__#/name:_________

			//if !findinlistbox add to listbox
			ListViewItem item = ifatl(commandname);

			item.SubItems[1].Text = fullname;
		}

		public void addMorph(String target, String message) {
		// morph #list gives us line by line of morph COMMAND and morph NAME

			//if !findinlistbox add to listbox
			ListViewItem item = ifatl(target);

			item.Checked = true; // mark that this was updated

			if (item.SubItems.Count != 2) {
				parent.boxErrar(target + " has " + item.SubItems.Count + " subitems instead of 1. Message was: " + message + "This should never be seen.");
			}

			// set message to given
			item.SubItems[1].Text = message;
		}

		public void addMorphStarting() {
		// when 'morph #list' starts, mark all items as unchecked and we check them off as they're updated, unchecked at the end get culled
			foreach ( ListViewItem i in listView.Items ) {
				i.Checked = false;
			}
		}

		public void addMorphDone() {
		// when 'morph #list' finishes, this is called so we can cull any morphs that have been culled from the muck but our client still thinks are there
			foreach ( ListViewItem i in listView.Items )
			{
				if (!i.Checked)
				{
					listView.Items.Remove(i);
				}
			}
			Show();
		}

		/// <summary>
		/// ifatl - If not Found in listbox, Add To Listbox
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		ListViewItem ifatl(String target)
		{
			// ifatl - If not Found in listbox, Add To Listbox

			target = target.ToLower();

			ListViewItem item = listView.FindItemWithText(target);

			// if findinlistbox return item^
			if (item != null) {
				//parent.boxPrint("already found " + item); //DEBUG message
				return item;
			}

			// if !findinlistbox add to listbox

			item = listView.Items.Add(target);
			item.SubItems.Add(" ");

			return item;
		}

		void MorphHelperWindow_HelpButtonClicked(object sender, CancelEventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(
				"This is the muck morph helper window, designed to make your use of the muck's Morph program easy. " +
				"Note: This window is not at all synced with the 'desc editor' window.\n\n" +

				"Morphs 101: A muck 'morph' is a snapshot of your current appearance, saved on the muck, that you can resume later. " +
				"To use a morph, first get your character looking like how you want it, and then click the 'save new morph' button. " +
				"A morph saves the following: description, scent, sex, species, say, and osay. It also stores a longer 'name' for your own reference, and action text that displays to the muck when you use that morph.\n\n" +

				"Right click on a morph in the list to see options: From the menu you can put on a morph, edit it, overwrite it, or delete it.\n\n" +

				"To 'edit' a morph, make it current, then edit your current appearance, and then overwrite the desired morph. " +
				"You can also edit a morph without putting it on by right clicking on it and selecting EDIT. This is handy for updating and managing a lot of morphs.\n\n" +

				"How this works, behind the scenes:\n" +
				"DagMU intercepts /_regmorphs, and also the output from the 'morph #list' command. Any morph adds are done through 'morph #add'.",
				"Morph Helper Help"
			);
			e.Cancel = true;
		}

		void morphToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0)
				return;

			parent.Send("morph " + listView.SelectedItems[0].Text, null);
		}

		void qMorphToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (listView.SelectedItems.Count == 0)
				return;

			parent.Send("qmorph " + listView.SelectedItems[0].Text, null);
		}

		void editToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Edit a morph in place, without morphing into it. Handy!

			if (listView.SelectedItems.Count == 0)
				return;

			parent.DescEditor.Show();
			parent.DescEditor.updateCommand(listView.SelectedItems[0].Text);
			//parent.DescEditor.buttonRefresh_Click(null, null);
		}

		void overwriteToolStripMenuItem_Click(object sender, EventArgs e)
		{
		// Copy over a selected morph with current desc

			if (listView.SelectedItems.Count == 0)
				return;

			if (System.Windows.Forms.MessageBox.Show("Are you sure you wish to overwrite the '" + listView.SelectedItems[0].Text + "' morph with your current appearance?", "Overwrite morph?", MessageBoxButtons.YesNo)
				!= System.Windows.Forms.DialogResult.Yes)
				return;

			// morph #add, overwriting

			parent.Send("morph #add", null);
			parent.Send(listView.SelectedItems[0].Text, null);
			parent.Send("yes", null);
			parent.Send(" ", null);
			parent.Send(" ", null);
		}

		void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// Delete a morph from the muck

			if (listView.SelectedItems.Count == 0)
				return;

			if (System.Windows.Forms.MessageBox.Show("Are you sure you wish to remove the '" + listView.SelectedItems[0].Name + "' morph from the muck, permenantly?", "Delete morph?", MessageBoxButtons.YesNo)
				!= System.Windows.Forms.DialogResult.Yes)
				return;

			parent.Send("morph #remove", null);
			parent.Send(listView.SelectedItems[0].Text, null);
			listView.SelectedItems[0].Remove();
			parent.Send("yes", null);
		}

		void buttonDescEditor_Click(object sender, EventArgs e)
		{
			parent.DescEditor.DescMode = true;
			parent.DescEditor.Show();
		}

		void buttonRefresh_Click(object sender, EventArgs e)
		{
			// get basic list of morphs, populate listbox with items representing morphs, then ask the muck details on the morphs to fill out the morph subitems
			// this should put world into morph list mode
			// line catcher should update this window automatically and then go back into normal mode

			parent.Send("morph #list", null); //TAPS

			//    to view list of morphs (smallest way)
			//  exa me=/_regmorphs
			//str /_regmorphs:  TIGER    DRAGON    WOLF    ELF    TEACHER          PURPLE          PANTSDRGN      SWIMDRGN    TEACHDRGN    DRGN    PANTSTGR    TGR    BUFFTGR    TEACHTGR    LABTGR    gothwolf    weredrgn    xmasptgr    minidrag    robedrgn    invis    mdragon    hallodrgn    sldrtgr    47drgn  
			//1 property listed.

			// We could use regmorphs because it's just one line and therefore more reliable and cheapto parse,
			// but that doesn't include the morph names like morph #list does and it's not bad for parsing accuracy
		}
		bool moremore;
		void buttonNewMorph_Click(object sender, EventArgs e)
		{
			// extend this window a bit to reveal textboxes

			if (!moremore) {
				// contracted, extend and reveal options
				togglewindow();
				textBoxCommand.Focus();
				return;
			}

			//  extended, will contract and apply the settings entered

			if (textBoxCommand.Text == "") {
				MessageBox.Show("You need to at least enter a command name for the morph..");
				return;
			}

			// check if command is found in listView, if so, tell user to right click and overwrite if he wants to overwrite
			ListViewItem temp = null;
			temp = listView.FindItemWithText(textBoxCommand.Text);
			if (temp != null) {
				listView.SelectedItems.Clear();
				temp.Selected = true;
				temp.Focused = true;
				listView.Focus();
				MessageBox.Show("This morph already exists. If you want to overwrite it, right click on it instead.");
				//buttonNewCancel_Click(nullptr, nullptr);
				return;
			}

			// send textbox info to world as morph #add
			// how to detect the 'this morph exists, overwrite?' thing? do we just check the entered commandname against the list and take it on faith? the consequences of a missed one aren't big, because we have a handy editor to fix it

			parent.Send("morph #add", null);
			parent.Send(textBoxCommand.Text, null);
			parent.Send(textBoxLongName.Text, null);
			parent.Send(textBoxMessage.Text, null);

			togglewindow(); // keep this last because it clears the textboxes
		}

		void togglewindow() {
			if (!moremore) {
				// contracted, extend!
				moremore = true;
				buttonNewMorph.Text = "> Click to save new morph! <";
				this.AcceptButton = buttonNewMorph;
				buttonNewCancel.Visible = true;
				label7.Top = 190;
				listView.Top = 206;
				listView.Height = this.Height - 50 - listView.Location.Y;
				textBoxCommand.Visible = true;
				textBoxLongName.Visible = true;
				textBoxMessage.Visible = true;
				label1.Visible = true;
				label2.Visible = true;
				label3.Visible = true;
				label4.Visible = true;
				label5.Visible = true;
				label6.Visible = true;
				return;
			}

			// extended, contract!
			moremore = false;
			buttonNewMorph.Text = "Save current appearance to NEW morph ...";
			this.AcceptButton = null;
			buttonNewCancel.Visible = false;
			label7.Top = 48;
			listView.Top = 64;
			listView.Height = this.Height - 50 - listView.Location.Y;
			textBoxCommand.Text = "";
			textBoxLongName.Text = "";
			textBoxMessage.Text = "";
			textBoxCommand.Visible = false;
			textBoxLongName.Visible = false;
			textBoxMessage.Visible = false;
			label1.Visible = false;
			label2.Visible = false;
			label3.Visible = false;
			label4.Visible = false;
			label5.Visible = false;
			label6.Visible = false;
		}

		void buttonNewCancel_Click(object sender, EventArgs e)
		{
			togglewindow();
		}
	}
}
