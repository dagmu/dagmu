using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DagMU.Model;

namespace DagMU.WPF
{
	class viewmodelWorld : World
	{
		#region viewmodel properties
		public String buttonConnectText { get { return Name; } }

		/// <summary>
		/// Is world loaded and managed
		/// </summary>
		public bool Active { get; set; }

		/// <summary>
		/// Is world in foreground
		/// </summary>
		public bool Selected { get; set; }
		#endregion

		#region construction
		public viewmodelWorld(String name) : base(name) { }
		#endregion
	}
}
