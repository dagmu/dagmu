using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagMU.Model.World
{
	public class Connection
	{
		public string Address { get; set; }
		public int Port { get; set; }
		public bool SSL { get; set; }
	}
}
