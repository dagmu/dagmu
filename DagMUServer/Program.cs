using System;
using System.Net;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Program
	{
		static void Main(string[] args)
		{
			Task.Run(() => new Server().Start());

			Console.ReadLine();
		}
	}
}
