using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DagMU.Forms.Helpers;
using System.Text.RegularExpressions;

namespace DagMU.Forms
{
	partial class WorldVM
	{
		/// <summary>
		/// You do not need to be connected to send fake muck text to this for testing
		/// </summary>
		void ProcLine(string s)
		{
			if (InvokeRequired) { this.Invoke((Action)(() => ProcLine(s))); return; }

			if (inLimbo)// If the muck is saving its database we turn back on as soon as it sends something new
				inLimbo = false;

			switch (status) {
				case MuckStatus.Intercepting_Connecting: Intercepting_Connecting1(s); return;
				case MuckStatus.Intercepting_Connecting2: Intercepting_Connecting2(s); return;
				case MuckStatus.Intercepting_Normal: Intercepting_Normal(s); return;
				case MuckStatus.Intercepting_ExaMe: Intercepting_ExaMe(s); return;
				case MuckStatus.Intercepting_CharName: Intercepting_CharName(s); return;
				case MuckStatus.Intercepting_CInfoMisc: Intercepting_CInfoMisc(s); return;
				case MuckStatus.Intercepting_CInfo: Intercepting_CInfo(s); return;
				case MuckStatus.Intercepting_WF: Intercepting_WF(s); return;
				case MuckStatus.Intercepting_Last: Intercepting_Last(s); return;
				case MuckStatus.Intercepting_Who: Intercepting_Who(s); return;
				case MuckStatus.Intercepting_WIFlags: Intercepting_WIFlags(s); return;
				case MuckStatus.Intercepting_Morph: Intercepting_Morph(s); return;
			}

			boxPrint("Errar! Unparsed line: " + s);
		}

		/// <summary>
		/// Brand new connection.
		/// Expecting the welcome.txt and login prompt.
		/// </summary>
		private void Intercepting_Connecting1(string s)
		{
			boxPrint(s);

			if (s.StartsWith(">> This notice put in for your protection from unwanted publications.")) {//TAPS
				// Ready to log in
				onReady();
				newStatus(MuckStatus.Intercepting_Connecting2);//wait for login
			}
		}

		/// <summary>
		/// Not sure if logged in...
		/// Either the MOTD or a connection error (full, locked, invalid user/pass).
		/// </summary>
		private void Intercepting_Connecting2(string s)
		{
			boxPrint(s);

			switch (s) {
				case "":
					return;
				case "Either that player does not exist, or has a different password.":
					connection.Disconnect();
					return;
				default:
				case "- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -":
					break;
			}

			onLoggedIn();

			newStatus(MuckStatus.Intercepting_Normal);// from here it is assumed login was successful
		}

		/// <summary>
		/// This is where we enter streammodes to deal with multiple line blocks.
		/// This is also where we deal with all the 1 line notices.
		/// </summary>
		private void Intercepting_Normal(string s)
		{
			if (isEchoEqual(s, "LOGGEDIN")) {
				onSynced();
				return;
			}

			if (s == "CharName set.")
				return;

			if (s == "CharName removed.")
				return;

			//  morph #list
			if (s == "Morph Hammer V1.2 (C) 1994 by Triggur") {
				newStatus(MuckStatus.Intercepting_Morph);
				morphsIntercepted = 0;
				MorphHelper.addMorphStarting();
				return;
			}

			// WHO list, so huge we don't want it in our main window
			if (s == "Name____________ Online__ Idle Doing___________________________________________") {//TAPS
				newStatus(MuckStatus.Intercepting_Who);
				Who.Show();
				Who.starting();
				return;
			}

			// WI #flags
			if (s == "-- WhatIsZ Extended V1.0.0.1 WF --------------------------------[ List flags ]") {//TAPS
				newStatus(MuckStatus.Intercepting_WIFlags);
				WIHelper.addstarting();
				return;
			}

			// Email verification: infrequent but annoying
			if (s == "### Please verify your email address!") {//TAPS
				Send("@email #verify yes"); //TAPS
				boxPrint("Email autoverified to the muck, please check console for correctness.");
				return;
			}

			if (s.StartsWith("###")) //TAPS
				return; // AFAIK only the email verify thing uses "###"

			// Limbo message
			if (s == "## Pausing to save database. This may take a while... ##") {//TAPS
				inLimbo = true;
				// UNDONE muck limbo reaction
				return;
			}

			// Last
			if (s == "Name              Laston             Last Connected    Last Disconnected") {//TAPS
				newStatus(MuckStatus.Intercepting_Last);
				boxPrint(s);
				return;
			}

			// cinfo
			if (s.StartsWith("> Character Info for: ")) {
				try {
					int len1 = ("> Character Info for: ").Length;
					int len2 = s.IndexOf(',');
					String charname;
					if (len2 > 0)
						charname = s.Substring(len1, len2 - len1);
					else
						charname = s.Substring(len1);

					cInfoInterceptingName = charname;

					if (s.Contains(", Miscellaneous field: ")) {
						CInfoHelperWindow cwin = FindMakeCInfoWindow(charname);

						cInfoInterceptingFieldName = s.Substring(s.IndexOf("field: ") + ("field: ").Length);

						newStatus(MuckStatus.Intercepting_CInfoMisc);
						return;
					}
				} catch { return; }

				newStatus(MuckStatus.Intercepting_CInfo);
				return;
			}

			//> Miscellaneous field "bou" removed.
			//> Miscellaneous field "blaaouou" removed.
			//> Miscellaneous field "blaa_ouou" set to "aoue".
			//> Miscellaneous field "blah" set to "rar".
			if (s.StartsWith("> Miscellaneous field \""))
				return;

			// WF on the fly information
			#region parsing onthe-fly WF information
			// WF: Hides/unhides
			if (s == "Adding #all to your hidefrom list.") {//TAPS
				wf.Hiding = WF.HiddenEnum.HidingFromAll;
				boxPrint("Now hidden from WF.");
				return;
			}

			if (s == "Removing #all from your hidefrom list.") {//TAPS
				wf.Hiding = WF.HiddenEnum.VisibleToAll;
				boxPrint("Now visible to WF.");
				return;
			}

			if (s.StartsWith("Hiding from: ")) {//TAPS
				if (s == "Hiding from: *everyone*")//TAPS
					wf.Hiding = WF.HiddenEnum.HidingFromAll;
				else
					wf.Hiding = WF.HiddenEnum.VisibleToAll;

				boxPrint(s);
				return;
			}

			// WF: The depressing message
			if (s == "No one that you are watching for is online.") {//TAPS
				wf.UpdateFullNoone();
				return;
			}

			// WF: Master WF List, always correct
			if (s == "Players online for whom you are watching:") {//TAPS
				boxPrint(s);
				newStatus(MuckStatus.Intercepting_WF);
				wf.UpdateFullStarting();
				return;
			}

			// WF: On-the-fly WF info, not always reliable
			if (s.StartsWith("Somewhere on the muck, ")) {//TAPS
				boxPrint(s);

				s = s.Substring("Somewhere on the muck, ".Length);//TAPS

				// Find the end of the name
				int i = 0;
				foreach (char c in s) {
					if (c == ' ')
						break;
					i++;
				}

				if ((s.Substring(i) == " has connected.") || (s.Substring(i) == " has reconnected.")) {//TAPS
					// They're almost definitely connected
					wf.UpdateOneConnected(s.Substring(0, i));
					return;
				}
				if (s.Substring(i) == " has disconnected.") {//TAPS
					wf.UpdateOneDisconnected(s.Substring(0, i));
					return;
				}

				boxPrint("Errar: malformed WF line");
				return;
			}
			#endregion
			// end WF stuff (at least for normal mode)

			if (s.StartsWith("str /")) {//TAPS
				Intercepting_Normal_Str(s);
				return;
			}

			// begin DIR property parsing
			if (s.StartsWith("dir /")) //TAPS
				return;

			//Arg:    {name:me}
			//Result: Dagon
			if (s == "Arg:    {name:me}") {
				newStatus(MuckStatus.Intercepting_CharName);
				return;
			}

			//exa me
			if (!String.IsNullOrEmpty(CharName)) {
				var regex = new Regex(@"(\w+)\(#(\d+)([A-Z]*)\)\s+(?:Sex toys:)\s+(\d+)\s*");//Shean(#152273PB)  Sex toys: 308  
				var matches = regex.Matches(s);
				if (matches.Count == 1 && matches[0].Groups.Count == 5) {
					//int characterDBRef = int.Parse(matches[0].Groups[2].Value);
					//string characterFlags = matches[1].Groups[3].Value;
					//int sexToys = int.Parse(matches[0].Groups[4].Value);

#if DEBUG
					boxPrint("First line   : " + s);
#else
					boxPrint(s);
#endif

					expectingActionsExits = false;
					expectingDescSettingLine = false;
					newStatus(MuckStatus.Intercepting_ExaMe);
					return;
				}
			}

			// detect '____ properties listed.' and surpress from window
			if (s.Length < 24) {// limit suppression abuse to just 4 ignored characters at front
				if (s.EndsWith(" properties listed."))
					return;
				if (s.EndsWith(" property listed."))
					return;
			}

			// Anything that makes it down this far into normal text is either normal text
			// or we just don't know how to interpret it yet!
			boxPrint(s);
		}

		/// <summary>
		/// str /
		/// Parsing str: current appearance, morph properties, lastpaged...
		/// </summary>
		private void Intercepting_Normal_Str(string s)
		{
			// get rid of "str /"
			s = s.Substring("str /".Length);

			//str /_regmorphs:  TIGER    DRAGON    WOLF    ELF    TEACHER          PURPLE          PANTSDRGN      SWIMDRGN    TEACHDRGN    DRGN    PANTSTGR    TGR    BUFFTGR    TEACHTGR    LABTGR    gothwolf    weredrgn    xmasptgr    minidrag    robedrgn    invis    mdragon    hallodrgn    sldrtgr    47drgn  
			if (s.StartsWith("_regmorphs:")) {
				// trim to just the list
				s = s.Substring(11);
				// and then pass it to MorphHelper
				MorphHelper.updateMorphs(s);
				return;
			}

			//str /ride/_mode:hand
			//str /ride/_mode:ride
			//str /ride/_mode:walk
			//str /ride/_mode:fly
			if (s.StartsWith("ride/_mode:")) {
				s = s.Substring(11);
				//find s in comboBoxRideMode
				int tmp = tbnRideMode.FindStringExact(s);//-1 not found, 0 empty, 1+ index found
				if ((tmp == -1) || (tmp == 0)) {
					// if not found, add it
					tbnRideMode.Items.Add(s);
					tmp = tbnRideMode.Items.Count - 1;
				}
				// now select it but don't trigger the thing that sets it on indexchange, set a bool?
				// HACK whats this do? comboBoxRideModeFlag = true;
				tbnRideMode.SelectedIndex = tmp;
			}

			//str /redesc#/1:test1
			//str /redesc#/2:Test2
			//str /redesc#/:4    <-- number of lines (we discard this)
			{
				var regex = new Regex(@"^(\w+)#/(\d+):(.+)$");//redesc#/2:Test2
				var matches = regex.Matches(s);
				if (matches.Count == 1 && matches[0].Groups.Count == 4 && !String.IsNullOrEmpty(matches[0].Groups[2].Value)) {
					string listName = matches[0].Groups[1].Value;
					if (characterDescLists.Count > 0 && characterDescLists.Contains(listName)) {
						DescEditor.updateLists(characterDescLists);
						DescEditor.DescMode = true;//editing a list that's used in our description
					}
					DescEditor.updateDesc(
						listName: listName,
						lineNum: int.Parse(matches[0].Groups[2].Value),
						text: matches[0].Groups[3].Value
					);
					return;
				}
			}

			//str /_scent:smells like dragon
			if (s.StartsWith("_scent:")) {
				DescEditor.DescMode = true;
				DescEditor.updateScent(s.Substring(7));
				return;
			}

			//str /species:anthro ice dragon
			if (s.StartsWith("species:")) {
				DescEditor.DescMode = true;
				DescEditor.updateSpecies(s.Substring(8));
				return;
			}

			//str /sex:male
			if (s.StartsWith("sex:")) {
				DescEditor.DescMode = true;
				DescEditor.updateGender(s.Substring(4));
				return;
			}

			//  exa me=/_say/
			//str /_say/format:You growl softly, "%m"
			//str /_say/oformat:growls softly, "%m"
			if (s.StartsWith("_say/oformat:")) {
				s = s.Substring(13);
				DescEditor.DescMode = true;
				DescEditor.updateSay(s);
				return;
			}
			//str /_say/format:You growl softly, "%m"
			if (s.StartsWith("_say/format:")) {
				s = s.Substring(12);
				DescEditor.DescMode = true;
				DescEditor.updateOSay(s);
				return;
			}

			//  exa me=/_page/lastpager
			//str /_page/lastpager:Anjire
			//  exa me=/_page/lastpaged
			//str /_page/lastpaged:Anjire
			if (s.StartsWith("_page/lastpaged:")) {
				lastPaged = s.Substring(16);
				return;
			}
			//str /_whisp/lastwhispered:Velos
			if (s.StartsWith("_whisp/lastwhispered:")) {
				lastWhispered = s.Substring(21);
				return;
			}

			if (s.StartsWith("_prefs/whatis/")) {
				Intercepting_Normal_Str_WhatIs(s);
				return;
			}

			if (s.StartsWith("morph#/")) {
				Intercepting_Normal_Str_Morph(s);
				return;
			}
		}

		/// <summary>
		/// str /_prefs/whatis/
		/// </summary>
		private void Intercepting_Normal_Str_WhatIs(string s)
		{
			s = s.Substring(14);

			// TODO add whatis#detail stuff
			//str /_prefs/whatis/wiEUsed?:yes
			//str /_prefs/whatis/1:vor plu unb sc mac dia inf
			//str /_prefs/whatis/2:bt ff bod wa snu le
			//str /_prefs/whatis/3:bon voy rim cas
			//str /_prefs/whatis/4:inc van tit age cbt cok
			//str /_prefs/whatis/5:an rom tea bi

			//if (s.StartsWith("used:")) { // is wi used or not
			//	wihelper.updateused(s.Substring(5));
			//	return;
			//}
			if (s.StartsWith("custom:")) { // custom field
				WIHelper.updatecustom(s.Substring(7));
				return;
			}

			if (s.StartsWith("flags:")) { // current flag settings
				WIHelper.updateflags(s.Substring(6));
				return;
			}
		}

		/// <summary>
		/// str /morph#/
		/// </summary>
		private void Intercepting_Normal_Str_Morph(string s)
		{
			//str /morph#/boxers#/desc#/:7
			//str /morph#/boxers#/desc#/1:test1
			//str /morph#/boxers#/desc#/2:Test2
			//str /morph#/boxers#/message:changes clothes!
			//str /morph#/boxers#/name:anthro boxer shorts nothin else
			//str /morph#/boxers#/osay:rumbles ~%m~
			//str /morph#/boxers#/say:You rumble ~%m~
			//str /morph#/boxers#/scent:The wolf smells like wolf, that's for certain. He may be of mixed heritidge, but he's all canine, and his lupine side has taken over his scent. His fur is clean though, at least within a few days.
			//str /morph#/boxers#/sex:male
			//str /morph#/boxers#/species:anthro dark wolf

			// trim what we tested for
			s = s.Substring(7);

			string[] words = s.Split(new String[] { "#/" }, StringSplitOptions.RemoveEmptyEntries);

			string morphCommand = words[0]; // this is the COMMAND name which is used as 'target' for the update functions-
			string listName = words[1];

			s = s.Substring(morphCommand.Length + 2);
			//desc#/:7
			//desc#/1:test1
			//species:anthro dark wolf

			if (listName == "desc") {
				s = s.Substring(6);
				//:7
				//1:test1
				//2:Test2

				int colonIndex1 = s.IndexOf(':');

				if (colonIndex1 == 0)
					return;		// we got ":7" which is the number of lines, which we don't care about

				int linenum = -1;
				try { linenum = int.Parse(s.Substring(0, colonIndex1)); } catch (Exception ex) { }

				DescEditor.DescMode = false;
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateLists(new List<string>() { listName });
				DescEditor.updateDesc(s.Substring(colonIndex1 + 1), linenum, listName); // overwrite

				return;
			} // end "desc#/blah"

			// handle the individual details (scent, sex, message, name, species, osay, say)

			int colonIndex = s.IndexOf(':');

			//species:anthro dark wolf

			String detail = s.Substring(0, colonIndex);	// species
			String data = s.Substring(colonIndex + 1);		// anthro dark wolf

			// note we use updateCommand each time to make sure the desc/morph editor is in the right state

			if (detail == "name") {
				MorphHelper.updateMorphName(morphCommand, data);
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateName(data);
				return;
			}
			if (detail == "message") {
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateMessage(data);
				return;
			}
			if (detail == "sex") {
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateGender(data);
				return;
			}
			if (detail == "species") {
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateSpecies(data);
				return;
			}
			if (detail == "scent") {
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateScent(data);
				return;
			}
			if (detail == "say") {
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateSay(data);
				return;
			}
			if (detail == "osay") {
				DescEditor.updateCommand(morphCommand);
				DescEditor.updateOSay(data);
				return;
			}

			boxPrint("errar: cannot parse morph detail: " + detail + " : " + data);
		}

		private void Intercepting_CharName(string s)
		{
			//Result: Dagon
			if (s.StartsWith("Result: "))
				CharName = s.Substring(("Result: ").Length);
			else
				ProcLine(s);

			newStatus(MuckStatus.Intercepting_Normal);
		}

		private void Intercepting_CInfo(string s)
		{
			// "> Done."
			// "Character: " + s
			// "> Miscellaneous fields: 40k, alts, anatomy, battlefield2, disclaimer, dom, dragonproportions, dvorak, elves, games, height, idle, magic, muckclient, power, secondlife, stallions, stmarys, tapsguide, turnoffs, turnons and whispers"
			CInfoHelperWindow cwindd = FindMakeCInfoWindow(cInfoInterceptingName);

			if (s == "> Done.") {
				newStatus(MuckStatus.Intercepting_Normal);
				cwindd.Show();
				cwindd.BringToFront();
				return;
			}

			if (s.StartsWith("> \"cinfo "))
				return;

			if (s.StartsWith("> Miscellaneous fields: ")) {
				try {
					s = s.Substring(("> Miscellaneous fields: ").Length);
					String[] strings = s.Split(new String[] { ", ", " and " }, StringSplitOptions.RemoveEmptyEntries);
					cwindd.UpdateMiscFields(strings);
				} catch { }
				return;
			}

			// normal cinfo field
			try {
				String fieldname = s.Substring(0, s.IndexOf(':'));
				String fieldtext = s.Substring(s.IndexOf(": ") + 1);
				while (fieldtext.StartsWith(" "))
					fieldtext = fieldtext.Substring(1);
				cwindd.UpdateField(fieldname, fieldtext, false);
			} catch { }
		}

		private void Intercepting_CInfoMisc(string s)
		{
			if (!s.StartsWith("> ")) {
				newStatus(MuckStatus.Intercepting_Normal);
				ProcLine(s);
				return;
			}

			try {
				s = s.Substring(2);
			} catch (ArgumentOutOfRangeException) { return; }

			FindMakeCInfoWindow(cInfoInterceptingName).UpdateField(cInfoInterceptingFieldName, s, true);

			newStatus(MuckStatus.Intercepting_Normal);
		}

		private void Intercepting_ExaMe(string s)
		{
			//second line
			if (expectingDescSettingLine) {
#if DEBUG
				boxPrint("Desc Line    : " + s);
#else
				boxPrint(s);
#endif

				//the current @desc me=, scraped from exame
				string characterDescSettingString = s.StartsWith("Key: ")
					? null
					: s;

				characterDescLists.Clear();

				var singleWordMatches = new Regex(@"^\w+$").Matches(characterDescSettingString);
				if (singleWordMatches.Count > 0) {
					//done, only expecting 1 match, so skip the next regex.
					foreach (Match match in singleWordMatches) {//'redesc'
						characterDescLists.Add(characterDescSettingString);
					}
				} else {
					foreach (Match match in new Regex(@"\{list:(\w+)\}").Matches(characterDescSettingString)) {//'blah blah {list:rar} blah {list:mar} blah'
						characterDescLists.Add(match.Groups[1].Value);
					}
				}

				DescEditor.updateLists(characterDescLists);

#if DEBUG
				boxPrint("Desc lists: " + String.Join(",", characterDescLists));
#endif

				expectingDescSettingLine = false;

				return;
			}

			//last n lines
			if (expectingActionsExits) {
				//HACK just eat the first one and go normal. don't know how many lines there will be and I don't want to send an echo to detect the end of the block
				expectingActionsExits = false;

#if DEBUG
				boxPrint("Actions Exits: " + s);
#else
				boxPrint(s);
#endif

				newStatus(MuckStatus.Intercepting_Normal);
				return;
			}

			//first line
			{
				//Type: PLAYER  Flags: BUILDER JUMP_OK
				var matches = new Regex(@"Type:\s+([A-Z]+)\s+Flags:\s+(.+)").Matches(s);
				if (matches.Count == 1 && matches[0].Groups.Count == 3) {
					//string objectType = matches[0].Groups[1].Value;//PLAYER
					//string objectFlags = matches[0].Groups[2].Value;//BUILDER JUMP_OK
					expectingDescSettingLine = true;

#if DEBUG
					boxPrint("Second line  : " + s);
#else
					boxPrint(s);
#endif

					return;
				}
			}

			//last line is 'No actions attached.' or 'Actions/exits:' and then lines of actions and exits
			if (s == "No actions attached.") {
			} else if (s == "Actions/exits:") {
				expectingActionsExits = true;

#if DEBUG
				boxPrint("Actions Exits: " + s);
#else
				boxPrint(s);
#endif

				return;
			} else {
#if DEBUG
				boxPrint("Middle Line  : " + s);
#else
				boxPrint(s);
#endif

				return;//could get stuck here!
			}

			newStatus(MuckStatus.Intercepting_Normal);
		}

		private void Intercepting_Last(string s)
		{
			//Name              Laston             Last Connected    Last Disconnected
			//---------------------------------------------------------------------------
			//Senjin            ( Today )          07:37PM 09/10/08  08:33PM 09/10/08

			//Name              Laston             Last Connected    Last Disconnected
			//---------------------------------------------------------------------------
			//Ayorith           --- ONLINE ---                       04:31PM 09/10/08

			if (s == "---------------------------------------------------------------------------")
				return;

			if (s.Length != 71) {
				// line doesn't match our recognition test, recycle it into procline to escape this trap
				newStatus(MuckStatus.Intercepting_Normal);
				ProcLine(s);
				return;
			}

			//Senjin            ( Today )          07:37PM 09/10/08  08:33PM 09/10/08
			//Werel             ( Last week )      07:49PM 08/31/08  10:04PM 08/31/08
			//Ashkii            --- ONLINE ---                       05:59AM 09/10/08
			//Tiger_ofthe_Wind  ( Yesterday )      09:21PM 09/09/08  09:45PM 09/09/08

			{
				// assume s is one of the meat lines, break it up and deal with it
				String tmpName = s.Substring(0, 18).TrimEnd(new Char[] { ' ' });		//0, 18		Ayorith
				String tmpStatus = s.Substring(18, 19).TrimEnd(new Char[] { ' ' });	//18, 19	--- ONLINE ---, ( Last week ), ( Yesterday ), ( Today ), ( 23 weeks ago )
				String tmpTime1 = s.Substring(37, 16);	//37, 16	07:49PM 08/31/08
				String tmpTime2 = s.Substring(55);		//55, -		10:04PM 08/31/08
				boxPrint("[" + tmpName + "]\n[" + tmpStatus + "]\n[" + tmpTime1 + "]\n[" + tmpTime2 + "]");
			}

			newStatus(MuckStatus.Intercepting_Normal);
		}

		private void Intercepting_Morph(string s)
		{
			//    to view list of morphs (hard way)
			//  morph #list
			//Morph Hammer V1.2 (C) 1994 by Triggur
			//-------------------------------------
			// 
			//         Command    ...morphs you to...
			//----------------------------------------------------------
			//[Q]MORPH PANTS      space pants!
			//[Q]MORPH BOXERS     anthro boxer shorts nothin else
			if (s.StartsWith("----"))
				return;

			if (s == "         Command    ...morphs you to...") {
				//boxPrint(s);
				return;
			}

			if (s.StartsWith("[Q]MORPH ")) {
				//boxPrint(s);
				s = s.Substring(9);
				//PANTS      space pants!
				int spaceindex = s.IndexOf(' ');
				//s.Substring(0,spaceindex);//PANTS
				//s.Substring(11);//space pants!
				morphsIntercepted++;
				MorphHelper.addMorph(s.Substring(0, spaceindex), s.Substring(11));
				return;
			}

			if (s == " " && morphsIntercepted == 0)
				return;// theres a single ' ' above the morph list

			if (s == "Enter the command associated with the morph you want to remove or '.' to abort.")
				return;

			if (s == "Enter a one-word command less than 10 characters to associate with this morph or '.' to abort.")
				return;

			if (s == "Enter the name of this morph, a single space for the default, or '.' to abort.")
				return;

			if (s == "Enter the message printed when you change to this shape, a single space for the default, or '.' to abort.")
				return;

			if (s == "(Do not type your name... it will be automatically inserted)")
				return;

			if (s.StartsWith("The current name of this morph is '"))
				return;

			if (s.StartsWith("You already have a morph associated with the '"))
				return;

			if (s == "Do you want to over-write it? (NO/yes)")
				return;

			if (s == "Enter the new name, a single space to keep this one, or '.' to abort.")
				return;

			if (s == "When you change to this shape it says '")
				return;

			if (s == "Enter the new message, a single space to keep this one, or '.' to abort.")
				return;

			// any other text gets recycled back to procline and we get out of this intercept trap
			newStatus(MuckStatus.Intercepting_Normal);
			//procline(s); // don't recycle to eat the last ' '
			MorphHelper.addMorphDone();
		}

		private void Intercepting_Who(string s)
		{
			if (Who == null)
				return;

			if (s.StartsWith("-- Total:")) {//TAPS
				s = s.Substring(9); //TAPS
				// HACK convert the first number in (s) into an int
				// we don't actually need the total, but i want to make sure the who is working right
				// and use for random useless statistics
				Who.done();
				newStatus(MuckStatus.Intercepting_Normal);
			}
			Who.newline(s);
		}

		private void Intercepting_WIFlags(string s)
		{
			// TODO text file
			// if file doesn't exist, create it
			// if it exists, overwrite it

			// write each line to the text file

			// then when done tell the wihelper to read it? or just update it on the way? hmm

			if (s.StartsWith("Flags are presently being maintained by : "))
				return;

			// now the only lines in the middle should be space delimeted groups of colon delimeted keyword:name pairs

			//"   !:no               a:available       ag:anything-goes  age:ageplay       " // 76 characters
			if (s.Length == 76) {// we assume this is a real line, not a very good check but hey
				string[] words = s.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				foreach (String word in words)
					WIHelper.addItem(word);

				return;
			}

			newStatus(MuckStatus.Intercepting_Normal);

			// any other text gets recycled back to procline and we get out of this intercept trap
			if (!s.StartsWith("----"))
				ProcLine(s);

			WIHelper.adddone();
		}

		private void Intercepting_WF(string s)
		{
			if (s == "Done.") {//TAPS
				newStatus(MuckStatus.Intercepting_Normal);
				wf.UpdateFullDone(); // the wf should have all the names its gonna get now
				return;
			}

			boxPrint(s);

			// must be the WF names, do a quick check to make sure it's a name line and not something else
			string[] words = s.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			// the names are space padded in collumns of 18, so we can do a quick check to make sure
			if ((s.Length % 18) != 0) {
				newStatus(MuckStatus.Intercepting_Normal);
				wf.UpdateFullAbort(); // tell the wf to take what it's got
				return;
			}

			// Must be WF names, parse them
			wf.UpdateFullNames(s);
		}
	}
}
