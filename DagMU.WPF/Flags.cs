using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlagNameT = System.Char;
using FlagTypeT = DagMUWPF.MuckFlags.FlagType;

namespace DagMUWPF
{
	public class MuckFlags
	{
		public IEnumerable<FlagNameT> GetFlagsByFlagType(FlagType type) { return Flags.Keys.Where(f => f.Type == type).Select(f => f.Name); }
		public IEnumerable<FlagTypeT> GetTypesByName(FlagNameT flag) { return Flags.Keys.Where(f => f.Name == flag).Select(f => f.Type); }
		public FlagProperty Get(Flag flag) { return Flags[flag]; }
		public FlagProperty Get(FlagNameT flag, FlagTypeT type) { return Flags[new Flag(flag, type)]; }
		//public FlagProperty GetByObjectType(ObjectType type) { return Flags.Values.Where(f => f.t; }

		public struct Flag {
			public readonly FlagNameT Name;
			public readonly FlagType Type;
			public Flag(char f, FlagType t) { this.Name = f; this.Type = t; }
		}

		public struct FlagProperty {
			public String Name;
			public String Description;
			public FlagProperty(String n, String d) { this.Name = n; this.Description = d; }
		}

		public enum ObjectType
		{
			Player,
			Room,
			Action,
			Thing,
			Program,
		}

		public enum FlagType
		{
			[Description("Player character")] Player,
			[Description("Place where players and things go")] Room,
			[Description("Action/Exit")] Action,
			[Description("MUF program")] Program,
			[Description("Any object besides a player. (thing, action, room, program)")] Object,
			[Description("Object that's not a player room or exit.")] Thing,
		}

		public Dictionary<Flag, FlagProperty> Flags = new Dictionary<Flag, FlagProperty>()
		{
			// from http://www.rdwarf.com/users/mink/muckman/flags.html
			{ new Flag('P', FlagType.Object), new FlagProperty("Player", "The object is a Player.")},
			{ new Flag('R', FlagType.Object), new FlagProperty("Room", "The object is a Room.")},
			{ new Flag('E', FlagType.Object), new FlagProperty("Exit", "The object is an Exit/Action.")},
			{ new Flag('F', FlagType.Object), new FlagProperty("MUCK Forth Program", "The object is a program.")},

			{ new Flag('A', FlagType.Room), new FlagProperty("Abode", "Anyone can set their home or the home of objects to the room.") },
			{ new Flag('A', FlagType.Action), new FlagProperty("Abate", "The exit is lower priority than an exit without the Abate flag. If compatible_priorties is tuned to `no', an abated exit's priority is `less than 0'; If compatible_priorites is tuned to `yes', an abated exit's priority is `less than 1'.")},
			{ new Flag('A', FlagType.Program), new FlagProperty("Autostart", "The program will automatically be loaded into memory and executed when the MUCK starts or restarts.")},

			{ new Flag('B', FlagType.Player), new FlagProperty("Builder", "Player can create and modify objects with the @create, @dig, @link, and @open. On most MUCKs, players start off with a B flag. A B flag on players is also called a `builder bit'.")},
			{ new Flag('B', FlagType.Room), new FlagProperty("Bound", "Personal exits (exits attached to a player) cannot be used in the room.") },
			{ new Flag('B', FlagType.Program), new FlagProperty("Block", "Any functions within the program run in preempt mode. If the program set B is called by another program, multi-tasking status returns to that of the calling program when execution exits from the called program.")},

			{ new Flag('C', FlagType.Player), new FlagProperty("Color", "On MUCK versions 5.x and lower, no effect. on 6.x, MUCK output will be formatted with ASCII color, provided that the player's client handles color and that the text has been formatted with color.")},
			{ new Flag('C', FlagType.Room), new FlagProperty("Chown_OK", "Anyone can take control of the object with the @chown command (`change ownership').")},
			{ new Flag('C', FlagType.Thing), new FlagProperty("Chown_OK", "Anyone can take control of the object with the @chown command (`change ownership').")},
			{ new Flag('C', FlagType.Action), new FlagProperty("Chown_OK", "Anyone can take control of the object with the @chown command (`change ownership').")},

			{ new Flag('D', FlagType.Room), new FlagProperty("Dark", "Players see only objects they own. If no objects would be seen, a `Contents' list is not appended to the description of the room. Wizards and the owner of the room see all objects normally.")},
			{ new Flag('D', FlagType.Thing), new FlagProperty("Dark", "The object does not appear in the room's `Contents' list.")},
			{ new Flag('D', FlagType.Player), new FlagProperty("Dark", "The player does not appear in the `Contents' list of rooms or in the WHO list. Only wizards may set players dark.")},
			{ new Flag('D', FlagType.Program), new FlagProperty("Debug", "A stack trace of internal program operations is printed out to anyone who uses the program.")},

			{ new Flag('H', FlagType.Player), new FlagProperty("Haven", "The player cannot be paged.")},
			{ new Flag('H', FlagType.Room), new FlagProperty("Haven", "The kill command may not be used in that room.")},
			{ new Flag('H', FlagType.Program), new FlagProperty("HardUID", "The program runs with the permissions of the owner of the trigger, rather than with the permissions of the user of the program. When this is set in conjunction with the Sticky (Setuid, below) flag on a program, and the program is owned by a wizard, then it will run with the effective mucker level and permissions of the calling program. If the caller was not a program, or the current program is not owned by a wizard, then it runs with Setuid.")},

			{ new Flag('J', FlagType.Room), new FlagProperty("Jump_OK", "Players can teleport to and from the room (assuming other conditions for teleporting are met). If the MUCK is configured with secure_teleporting, J indicates that exits attached to players and objects can be used to leave to leave the room, and !J indicates that they cannot.")},
			{ new Flag('J', FlagType.Thing), new FlagProperty("Jump_OK", "The object can be moved by a program running at any Mucker level.")},
			{ new Flag('J', FlagType.Player), new FlagProperty("Jump_OK", "The player can teleport to and from rooms (assuming other conditions for teleporting are met).")},

			{ new Flag('K', FlagType.Player), new FlagProperty("Kill_OK", "The player can be killed with the kill command. A player who is `killed' is simply sent home.")},

			{ new Flag('L', FlagType.Room), new FlagProperty("Link_OK", "Anyone can link exits to the room.")},
			{ new Flag('L', FlagType.Program), new FlagProperty("Link_OK", "The program can be called by any program, and can be triggered by actions and propqueues not owned by the owner of the program.")},

			{ new Flag('Q', FlagType.Room), new FlagProperty("Quell", "The Quell flag cancels the effects of a wizard flag. A wizard player set Q is effectively a normal player. A Q flag on a wizbitted room will cancel the realms-wiz powers of the room's owner.")},
			{ new Flag('Q', FlagType.Player), new FlagProperty("Quell", "The Quell flag cancels the effects of a wizard flag. A wizard player set Q is effectively a normal player. A Q flag on a wizbitted room will cancel the realms-wiz powers of the room's owner.")},

			{ new Flag('S', FlagType.Thing), new FlagProperty("Sticky", "The object will return to its home when dropped.")},
			{ new Flag('S', FlagType.Room), new FlagProperty("Silent", "The room's drop-to is delayed until all players have left the room.")},
			{ new Flag('S', FlagType.Player), new FlagProperty("Silent", "The player will not see dbrefs on things she owns, and will not see objects in a Dark room. Control is unchanged however.")},
			{ new Flag('S', FlagType.Program), new FlagProperty("SetUID", "The program runs with the permissions of the owner of the program, and not those of the user.")},

			{ new Flag('W', FlagType.Room), new FlagProperty("Wizard", "The room's owner has Realms Wiz powers in that room and any rooms parented to it, provided that the MUCK's realms_control parameter is set to `yes'.")},
			{ new Flag('W', FlagType.Action), new FlagProperty("Wizard", "The exit runs at priority 4.")},
			{ new Flag('W', FlagType.Player), new FlagProperty("Wizard", "The player is a wizard. Wizards have control over all objects in the database (although with some restrictions in their control over God and other wizards). Wizards can use restricted, wiz-only commands, and can set programs, rooms, and things W and B. Some wizard powers are enabled or disabled by the system parameter god_priv.")},
			{ new Flag('W', FlagType.Program), new FlagProperty("Wizard", "The program is effectively Mucker level 4. All MUF primitives may be used, and do not have a maximum instruction count unless the program is running in preempt mode.")},

			{ new Flag('X', FlagType.Player), new FlagProperty("Xforcicble", "The player may be forced by a player (or other object type) to which it is force_locked.")},
			{ new Flag('X', FlagType.Thing), new FlagProperty("Xforcicble", "The thing may be forced by a player (or other object type) to which it is force_locked.")},

			{ new Flag('V', FlagType.Thing), new FlagProperty("Vehicle", "The object is a vehicle.")},
			{ new Flag('V', FlagType.Room), new FlagProperty("Vehicle", "Vehicles may not enter the room.")},
			{ new Flag('V', FlagType.Action), new FlagProperty("Vehicle", "Vehicles may not use the exit.")},

			{ new Flag('Z', FlagType.Thing), new FlagProperty("Zombie", "The object is a Zombie: all output the Zombie sees or hears will be related to the controlling player.")},
			{ new Flag('Z', FlagType.Room), new FlagProperty("Zombie", "Zombies may not enter the room or be forced in the room.")},
			{ new Flag('Z', FlagType.Action), new FlagProperty("Zombie", "Zombies may not use the exit.")},

			/*
			 * from http://www.rdwarf.com/users/mink/muckman/flags.html
			M1 (Mucker Level 1) (See also Section 3.2.1)
			On a Player: The player is an `apprentice' Mucker. He can use the MUF editor and create M1 programs.
			On an Exit: The exit runs at priority 1.
			On a Program: The program runs with Mucker level 1 permissions. The program cannot get information about or send information to any object that is not in the same room. Some MUF primitives cannot be used. Program output to anyone except the triggering player is prepended with the triggering player's name. Instruction count is limited to about 20,000 instructions. The program follows permissions for protected props (see Section 2.1.1).

			M2 (Mucker Level 2) (See also Section 3.2.1)
			On a Player: The player is a `journeyman' Mucker. She can use the MUF editor and create M2 programs. She can set the Mucker level of any program she controls to M1 or M2.
			On an Exit: The exit runs at priority 2.
			On a Program: The program runs with Mucker level 2 permissions. Some MUF primitives cannot be used. Instruction count is limited to about 80,000 instructions. The program follows permissions for protected props (see Section 2.1.1).

			M3 (Mucker Level 3) (See also Section 3.2.1)
			On a Player: The player is a `master' Mucker. He can use the MUF editor and create M3 programs. He can set the Mucker level of any program he controls to M1, M2, or M3.
			On an Exit: The exit runs at priority 3.
			On a Program: The program runs with Mucker level 3 permissions. Almost all MUF primitives can be used. There is no absolute limit to instruction count, unless the program is running in PREEMPT mode. The program may over-ride the permissions for protected props.
			*/
		};
	}
}
