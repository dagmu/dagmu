using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagMU.Model
{
	public interface IConsole : IHelper
	{
		void Print(string line);
	}
}
