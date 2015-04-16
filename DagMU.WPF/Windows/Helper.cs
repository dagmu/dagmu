using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DagMU.Model;
using System.Windows;

namespace DagMUWPF.Windows
{
	public abstract class Helper : Window, IHelper
	{
		internal int index;

		void IHelper.SetIndex(int i)
		{
			index = i;
		}
	}
}
