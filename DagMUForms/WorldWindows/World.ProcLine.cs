using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DagMU.HelperWindows;

namespace DagMU
{
	partial class World
	{
		void procline(string s)
		{
			// You do not need to be connected to send fake muck text to this for testing

			// if we're in the wrong thread, pass the message to the right thread
			if (this.InvokeRequired) {
				this.Invoke(new StringDelegate(procline), s);
				return;
			}

			String[] words;
			int colonindex;

			if (inlimbo)// If the muck is saving its database we turn back on as soon as it sends something new
				inlimbo = false;

			switch (status) {

				case MuckStatus.intercepting_connecting:
					// intercepting_connecting
					// Brand new connection, expecting the welcome.txt
					boxprint(s);
					if (!s.StartsWith(">> This notice put in for your protection from unwanted publications.")) //TAPS
						return;

					// Ready to log in
					if (prefs.sendOnConnect != null) {
						connection.Send(prefs.sendOnConnect);
					}
					newStatus(MuckStatus.intercepting_connecting2);
					// UNDONE newinputbox(3); // make a 3 line input box
					return;
				// end case intercepting_connecting

				case MuckStatus.intercepting_connecting2:
					// intercepting_connecting2
					// Either the MOTD or a connection error (full, locked, invalid user/pass)
					// Login sent
					boxprint(s);
					if (s == "Either that player does not exist, or has a different password.") {//TAPS
						connection.Disconnect();
						return;
					}

					Echo("LOGGEDIN");

					newStatus(MuckStatus.intercepting_normal);// from here it is assumed login was successful

					// TODO check for invalid login messages
					return;
				// end case intercepting_connecting2


				case MuckStatus.intercepting_normal:
					// This is where we enter streammodes to deal with multiple line blocks
					// This is also where we deal with all the 1 line notices

					if (isecho(s, "LOGGEDIN")) {
						boxofmucktext.Clear();
						boxofmucktext.ClearUndo();

						connection.Send("exa me=/_page/lastpaged");
						connection.Send("exa me=/_whisp/lastwhispered");
						connection.Send("exa me=/ride/_mode");
						connection.Send("wf #hidefrom");

						connection.Send("@mpi {name:me}");//get character name on connect!

						connection.Send("look");//TAPS
						return;
					}

					if (s == "CharName set.")
						return;

					if (s == "CharName removed.")
						return;

					//  morph #list
					if (s == "Morph Hammer V1.2 (C) 1994 by Triggur") {
						newStatus(MuckStatus.intercepting_morph);
						morphsintercepted = 0;
						MorphHelper.addMorphStarting();
						return;
					}



					// WHO list, so huge we don't want it in our main window
					if (s == "Name____________ Online__ Idle Doing___________________________________________") {//TAPS
						newStatus(MuckStatus.intercepting_who);
						who.Show();
						who.starting();
						return;
					}

					// WI #flags
					if (s == "-- WhatIsZ Extended V1.0.0.1 WF --------------------------------[ List flags ]") {//TAPS
						newStatus(MuckStatus.intercepting_wiflags);
						WIHelper.addstarting();
						return;
					}


					// Email verification: infrequent but annoying
					if (s == "### Please verify your email address!") {//TAPS
						connection.Send("@email #verify yes"); //TAPS
						boxprint("Email autoverified to the muck, please check console for correctness.");
						return;
					}

					if (s.StartsWith("###")) //TAPS
						return; // AFAIK only the email verify thing uses "###"



					// Limbo message
					if (s == "## Pausing to save database. This may take a while... ##") {//TAPS
						inlimbo = true;
						// UNDONE muck limbo reaction
						return;
					}



					// Last
					if (s == "Name              Laston             Last Connected    Last Disconnected") {//TAPS
						newStatus(MuckStatus.intercepting_last);
						boxprint(s);
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

							cinfointerceptingname = charname;

							if (s.Contains(", Miscellaneous field: ")) {
								CInfoHelperWindow cwin = FindMakeCInfoWindow(charname);

								cinfointerceptingfieldname = s.Substring(s.IndexOf("field: ") + ("field: ").Length);

								newStatus(MuckStatus.intercepting_cinfomisc);
								return;
							}
						} catch { return; }

						newStatus(MuckStatus.intercepting_cinfo);
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
						boxprint("Now hidden from WF.");
						return;
					}

					if (s == "Removing #all from your hidefrom list.") {//TAPS
						wf.Hiding = WF.HiddenEnum.VisibleToAll;
						boxprint("Now visible to WF.");
						return;
					}

					if (s.StartsWith("Hiding from: ")) {//TAPS
						if (s == "Hiding from: *everyone*")//TAPS
							wf.Hiding = WF.HiddenEnum.HidingFromAll;
						else
							wf.Hiding = WF.HiddenEnum.VisibleToAll;

						boxprint(s);
						return;
					}


					// WF: The depressing message
					if (s == "No one that you are watching for is online.") {//TAPS
						wf.UpdateFullNoone();
						return;
					}


					// WF: Master WF List, always correct
					if (s == "Players online for whom you are watching:") {//TAPS
						newStatus(MuckStatus.intercepting_wf);
						wf.UpdateFullStarting();
						return;
					}


					// WF: On-the-fly WF info, not always reliable
					if (s.StartsWith("Somewhere on the muck, ")) {//TAPS
						s = s.Substring(23);//TAPS

						// Find the end of the name, since .net doesn't have a trimright
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

						boxprint("Errar: malformed WF line");
						return;
					}
					#endregion
					// end WF stuff (at least for normal mode)

					// begin STR property parsing
					if (s.StartsWith("str /")) {//TAPS
					#region parsing str: current appearance, morph properties, lastpaged
						// get rid of "str /"
						s = s.Substring(5);

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
						if (s.StartsWith("redesc#/")) {
							#region parsing str redesc
							// trim what we just tested for
							s = s.Substring(8);

							//:2    <-- number of lines (we discard this)
							//1:test1
							//2:Test2

							colonindex = s.IndexOf(':');

							if (colonindex == 0)
								return;		// we got ":2" meaning number of lines in the list, which we don't care about

							DescEditor.DescMode = true;
							try {
								String theline = s.Substring(colonindex + 1);
								int linenum = -1;
								linenum = int.Parse(s.Substring(0, colonindex));
								DescEditor.updateDesc(s.Substring(colonindex + 1), linenum);
							} catch {
								newStatus(MuckStatus.intercepting_normal);
								return;
							}
							/*if (s.Substring(0,colonindex) == "1") {
								DescEditor.updateDesc(s.Substring(colonindex+1), true); // overwrite
								//boxprint("overwrite " + s.Substring(colonindex+1));
							}
							else {
								DescEditor.updateDesc(s.Substring(colonindex+1), false);  // append
								//boxprint("append " + s.Substring(colonindex+1));
							}*/

							return;

							#endregion
						} // end if (s.StartsWith("redesc#/"))

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
							lastpaged = s.Substring(16);
							return;
						}
						//str /_whisp/lastwhispered:Velos
						if (s.StartsWith("_whisp/lastwhispered:")) {
							lastwhispered = s.Substring(21);
							return;
						}

						//str /_prefs/whatis/
						#region str /_prefs/whatis/
						if (s.StartsWith("_prefs/whatis/")) {
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

							return;
						#endregion
						} // end /_prefs/whatis/

						//str /morph#/
						#region parsing str /morph folder
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
						if (s.StartsWith("morph#/")) {
							// trim what we tested for
							s = s.Substring(7);

							words = s.Split(new String[] { "#/" }, StringSplitOptions.RemoveEmptyEntries);

							String morphcommand = words[0]; // this is the COMMAND name which is used as 'target' for the update functions-

							s = s.Substring(morphcommand.Length + 2);
							//desc#/:7
							//desc#/1:test1
							//species:anthro dark wolf

							if (words[1] == "desc") {
								s = s.Substring(6);
								//:7
								//1:test1
								//2:Test2

								colonindex = s.IndexOf(':');

								if (colonindex == 0)
									return;		// we got ":7" which is the number of lines, which we don't care about

								int linenum = -1;
								try { linenum = int.Parse(s.Substring(0, colonindex)); } catch (Exception ex) { }

								DescEditor.DescMode = false;
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateDesc(s.Substring(colonindex + 1), linenum); // overwrite

								return;
							} // end "desc#/blah"

							// handle the individual details (scent, sex, message, name, species, osay, say)

							colonindex = s.IndexOf(':');

							//species:anthro dark wolf

							String detail = s.Substring(0, colonindex);	// species
							String data = s.Substring(colonindex + 1);		// anthro dark wolf

							// note we use updateCommand each time to make sure the desc/morph editor is in the right state

							if (detail == "name") {
								MorphHelper.updateMorphName(morphcommand, data);
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateName(data);
								return;
							}
							if (detail == "message") {
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateMessage(data);
								return;
							}
							if (detail == "sex") {
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateGender(data);
								return;
							}
							if (detail == "species") {
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateSpecies(data);
								return;
							}
							if (detail == "scent") {
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateScent(data);
								return;
							}
							if (detail == "say") {
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateSay(data);
								return;
							}
							if (detail == "osay") {
								DescEditor.updateCommand(morphcommand);
								DescEditor.updateOSay(data);
								return;
							}

							boxprint("errar: cannot parse morph detail: " + detail + " : " + data);
							return;

						#endregion
						} // end if (s.StartsWith("morph#/"))

						return;
					#endregion
					} // end STR property parsing

					// begin DIR property parsing
					if (s.StartsWith("dir /")) //TAPS
						return;

					//Arg:    {name:me}
					//Result: Dagon
					if (s == "Arg:    {name:me}") {
						newStatus(MuckStatus.intercepting_charname);
						return;
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
					boxprint(s);
					return;
				// end case intercepting_normal ----------------------------------------------------------------/
				////////////////////////////////////////////////////////////////////////////////////////////////


				case MuckStatus.intercepting_charname:
					//Result: Dagon
					if (s.StartsWith("Result: "))
						CharName = s.Substring(("Result: ").Length);
					else
						procline(s);

					newStatus(MuckStatus.intercepting_normal);
					return;


				case MuckStatus.intercepting_cinfomisc:
					if (!s.StartsWith("> ")) {
						newStatus(MuckStatus.intercepting_normal);
						procline(s);
						return;
					}

					try {
						s = s.Substring(2);
					} catch (ArgumentOutOfRangeException) { return; }

					CInfoHelperWindow cwind = FindMakeCInfoWindow(cinfointerceptingname);
					cwind.UpdateField(cinfointerceptingfieldname, s, true);
					newStatus(MuckStatus.intercepting_normal);
					return;


				case MuckStatus.intercepting_cinfo:
					// "> Done."
					// "Character: " + s
					// "> Miscellaneous fields: 40k, alts, anatomy, battlefield2, disclaimer, dom, dragonproportions, dvorak, elves, games, height, idle, magic, muckclient, power, secondlife, stallions, stmarys, tapsguide, turnoffs, turnons and whispers"
					CInfoHelperWindow cwindd = FindMakeCInfoWindow(cinfointerceptingname);

					if (s == "> Done.") {
						newStatus(MuckStatus.intercepting_normal);
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
					return;


				case MuckStatus.intercepting_wf:
					if (s == "Done.") {//TAPS
						newStatus(MuckStatus.intercepting_normal);
						wf.UpdateFullDone(); // the wf should have all the names its gonna get now
						return;
					}

					// must be the WF names, do a quick check to make sure it's a name line and not something else
					words = s.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					// the names are space padded in collumns of 18, so we can do a quick check to make sure
					if ((s.Length % 18) != 0) {
						newStatus(MuckStatus.intercepting_normal);
						wf.UpdateFullAbort(); // tell the wf to take what it's got
						return;
					}

					// Must be WF names, parse them
					wf.UpdateFullNames(s);
					return;
				// end case intercepting_wf



				case MuckStatus.intercepting_last:
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
						newStatus(MuckStatus.intercepting_normal);
						procline(s);
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
						boxprint("[" + tmpName + "]\n[" + tmpStatus + "]\n[" + tmpTime1 + "]\n[" + tmpTime2 + "]");
					}
					newStatus(MuckStatus.intercepting_normal);
					return;
					// end case intercepting_last



				case MuckStatus.intercepting_who:
					if (who == null)
						return;

					if (s.StartsWith("-- Total:")) {//TAPS
						s = s.Substring(9); //TAPS
						// HACK convert the first number in (s) into an int
						// we don't actually need the total, but i want to make sure the who is working right
						// and use for random useless statistics
						who.done();
						newStatus(MuckStatus.intercepting_normal);
					}
					who.newline(s);
					return;
					// end case intercepting_who



				case MuckStatus.intercepting_wiflags:
					// TODO text file
					// if file doesn't exist, create it
					// if it exists, overwrite it

					// write each line to the text file

					// then when done tell the wihelper to read it? or just update it on the way? hmm

					if (s.StartsWith("Flags are presently being maintained by : "))
						return;

					// now the only lines in the middle should be space delimeted groups of colon delimeted keyword:name pairs

					//"   !:no               a:available       ag:anything-goes  age:ageplay       " // 76 characters
					if (s.Length == 76)	{// we assume this is a real line, not a very good check but hey
						words = s.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						foreach (String word in words)
							WIHelper.additem(word);

						return;
					}

					// any other text gets recycled back to procline and we get out of this intercept trap
					newStatus(MuckStatus.intercepting_normal);

					if (!s.StartsWith("----"))
						procline(s);

					WIHelper.adddone();
					return;

				case MuckStatus.intercepting_morph:
					#region intercepting morph
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
						//boxprint(s);
						return;
					}

					if (s.StartsWith("[Q]MORPH ")) {
						//boxprint(s);
						s = s.Substring(9);
						//PANTS      space pants!
						int spaceindex = s.IndexOf(' ');
						//s.Substring(0,spaceindex);//PANTS
						//s.Substring(11);//space pants!
						morphsintercepted++;
						MorphHelper.addMorph(s.Substring(0, spaceindex), s.Substring(11));
						return;
					}

					if (s == " " && morphsintercepted == 0)
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
					newStatus(MuckStatus.intercepting_normal);
					//procline(s); // don't recycle to eat the last ' '
					MorphHelper.addMorphDone();
					return;

					#endregion
			}// switch

			boxprint(s);
		}
	}
}
