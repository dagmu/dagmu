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
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);

			e.Cancel = true;
			this.Hide();
		}
	}
}
