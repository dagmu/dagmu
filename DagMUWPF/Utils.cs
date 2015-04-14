using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagMU.WPF
{
	class Utils
	{
		static public String AssemblyDirectory
		{
			get
			{
				string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				string path = Uri.UnescapeDataString(uri.Path);//remove the File:// at the beginning
				return Path.GetDirectoryName(path);//change to the normal windows format
			}
		}
	}
}
