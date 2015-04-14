using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DagMU.WPF
{
	class InputBoxBox : StackPanel
	{
		public void Refocus()
		{
			if (this.Children.Count > 0)
				Children[0].Focus();
		}
	}
}
