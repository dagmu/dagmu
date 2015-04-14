using System;

namespace DagMU.Model
{
	[Serializable]
	public class World
	{
		#region construction
		public World() { Name = String.Empty; }
		public World(String value) { Name = value; }
		#endregion

		public Muck Muck { get; set; }
		public String Name { get; set; }
	}
}
