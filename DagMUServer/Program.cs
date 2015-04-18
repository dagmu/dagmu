using System;
using System.Net;

namespace DagMUServer
{
	class Program
	{
		const int port = 2069;

		static void Main(string[] args)
		{
			IPAddress IPv4 = IPAddress.Any;
			//IPAddress IPv6 = Dns.GetHostEntry("127.0.0.1").AddressList[0];
			new Server().Start(IPv4, port);

			Console.ReadLine();
		}
	}
}
