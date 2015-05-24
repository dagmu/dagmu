using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DagMU.Model
{
	public class Data
	{
		public ObservableCollection<TextMatch> stuffToMatch = new ObservableCollection<TextMatch>() {
			new TextMatch(new Regex(@"^[\w]+ (page(?:s|(?:-pose))?), \"".*\"" to [\w]*[.]?$"), new ColorRGB(205,92,92)),
			new TextMatch(new Regex(@"^(?:In a )(page-pose)(?: to you, \w+ .+)$"), new ColorRGB(205,92,92)),
			new TextMatch(new Regex(@"^[\w]+ (whisper(?:s?)), \"".*\"" to [\w]*.$"), new ColorRGB(72,209,204)),
			new TextMatch(new Regex(@"((?:[A-Za-z0-9_\-]+) (?:has (?:(?:dis|re|)connected|left|arrived)|(?:goes home)|(?:concentrates on a distant place, and )(?:fades from sight)|(?:(?:is )(?:taken home)(?: to sleep by the local police))).)$"), ColorRGB.Grey),
			new TextMatch(new Regex(@"^(Somewhere on the muck, (?:[A-Za-z0-9_\-]+) has (?:(?:|re|dis)connected).)$"), ColorRGB.Grey),
		};

		public ObservableCollection<TextMatch> namesToMatch = new ObservableCollection<TextMatch>() {
			new TextMatch("Shean", ColorRGB.Teal),
			new TextMatch("Dagon", ColorRGB.MediumPurple),
			new TextMatch("Yko", ColorRGB.Yellow),
			new TextMatch("Szai", ColorRGB.Yellow),
			new TextMatch("Mkosi", ColorRGB.Orange),
		};

		public class TextMatch
		{
			public TextMatch(string value, ColorRGB color) : this(color) { Match = value; }
			public TextMatch(Regex regex, ColorRGB color) : this(color) { Regex = regex; }
			public TextMatch(ColorRGB color) { this.Color = color; }

			public String Match
			{
				get { return match; }
				set { regex = null; match = value; }
			}

			public Regex Regex
			{
				get { if (regex == null) regex = new Regex(@"\b" + match + @"\b"); return regex; }
				set { regex = value; }
			}

			public String str { get { return "blah"; } }

			public ColorRGB Color;

			Regex regex = null;
			string match;
		}

		public struct TextMatchPlace
		{
			public int Index, Length;
			public ColorRGB Color;
		}

		public struct ColorRGB
		{
			public int R, G, B;

			public ColorRGB(int r, int g, int b)
			{
				R = r; G = g; B = b;
			}

			public static ColorRGB Grey { get { return new ColorRGB(128, 128, 128); } }
			public static ColorRGB MediumPurple { get { return new ColorRGB(147, 112, 219); } }
			public static ColorRGB Orange { get { return new ColorRGB(255, 102, 0); } }
			public static ColorRGB Teal { get { return new ColorRGB(0, 128, 128); } }
			public static ColorRGB Yellow { get { return new ColorRGB(255, 215, 0); } }
		}
	}
}
