using System;
using System.Net;

namespace DagMUServer
{
	class Program
	{
		const int port = 2069;

		//much mess
		//needs more https://msdn.microsoft.com/en-us/magazine/dn605876.aspx
		static void Main(string[] args)
		{
			IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];

			var server = new Server();
			server.Start(ipAddress, port);
			Console.WriteLine(String.Format("Listening on {0}:{1}", ipAddress.ToString(), port));
			Console.ReadLine();
		}
	}
}
