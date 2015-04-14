using System;
using System.Collections.Generic;

namespace DagMU.Forms
{
	[Serializable]
	public class Fur
	{
		public Fur(String name)
		{
			Name = name; // must have a name, all else is optional
		}

		/// <summary>
		/// this character's name; primary key
		/// </summary>
		public String Name;

		/// <summary>
		/// if this character is an alt of someone, you can point to the main character here
		/// </summary>
		public String AltOf;

		/// <summary>
		/// "horses", "dragons", whatever user wants to use to sort his friends
		/// </summary>
		public String Group;

		/// <summary>
		/// gmt, pst, est
		/// </summary>
		public String TimeZone;

		/// <summary>
		/// when met on this muck?
		/// </summary>
		public DateTime WhenMet;

		/// <summary>
		/// should this fur be highlighted or unhighlighted on the screen?
		/// more properties like beeping on paging can go here, as well as defaults like tear-away all pages.
		/// </summary>
		public int MyPreference;

		/// <summary>
		/// personal optional notes for a fur
		/// </summary>
		public String Notes;

		// this is updated whenever you look at the fur, as it is more subject to change than the above properties
		public String Species;
		public String Gender;
		public String Description;
		public String Smell;

		public String WI;
		public Dictionary<String, String> CInfo;

		/// <summary>
		/// used internally to track online status
		/// </summary>
		[NonSerialized]
		public int Quantity;

		[NonSerialized]
		public System.Drawing.Image PicThumb;

		[NonSerialized]
		public System.Drawing.Image Pic;
	}
}
