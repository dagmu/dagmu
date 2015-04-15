using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagMU.Forms.HelperWindows
{
	public class HelperWindow : IHelper
	{
		void IHelper.Show()
		{
			throw new NotImplementedException();
		}
	}

	public interface IHelper
	{
		void Show();
	}
}
