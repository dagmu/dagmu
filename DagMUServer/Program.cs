using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Program
	{
		const int port = 2069;
		
		//much mess
		//needs more https://msdn.microsoft.com/en-us/magazine/dn605876.aspx
		static void Main(string[] args)
		{
			var listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
			listener.Start();

			var taskCancel = new CancellationTokenSource();
			var task = Accept(listener, taskCancel.Token);
			task.Start();

			Console.ReadKey(true);
			taskCancel.Cancel();
			Console.ReadKey(true);
		}

		static async Task Accept(TcpListener listener, CancellationToken cancelToken)
		{
			/*while (!cancelToken.IsCancellationRequested) {
				TcpClient connection = await listener.AcceptTcpClientAsync();

				Task t = .Factory.StartNew(async () => {
					while (connection.Connected && !cancelToken.IsCancellationRequested) {
						string msg = await connection.
					}
				})
			}*/
		}
	}

	class Server
	{
		public void Server(TcpListener listener, CancellationToken cancelToken)
		{

		}
	}
}
