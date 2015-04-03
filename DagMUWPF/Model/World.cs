using System;

namespace DagMUWPF.Model
{
	[Serializable]
	public class World
	{
		#region construction
		public World() { Name = String.Empty; }
		public World(String one) { Name = one; }
		#endregion

		public Muck Muck { get; set; }
		public String Name { get; set; }
	}
}
