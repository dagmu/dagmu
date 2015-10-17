using System;

namespace Model
{
	[Serializable]
	public class World
	{
		public World() : this(string.Empty) { }

		public World(string value)
		{
			Name = value;
		}

		public string Name { get; set; }
		public string NameShort { get; set; }

		public string QUITString { get; set; }
	}
}
