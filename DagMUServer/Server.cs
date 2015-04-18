using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Server
	{
		public async Task Start()
		{
			await Start(IPAddress.Any);
		}

		public async Task Start(IPAddress ipAddress, int port = 2069)
		{
			Log(typeof(Program).Assembly.GetName().Name);
			listener = new TcpListener(port);
			listener.Start();
			Log("Listening on {0} : {1}", ipAddress.ToString(), port);
			while (true) {
				await acceptClient();
			}
		}

		private TcpListener listener;
		private Dictionary<string, Client> clients = new Dictionary<string, Client>();

		private async Task acceptClient()
		{
			var client = await listener.AcceptTcpClientAsync();
			var id = Guid.NewGuid().ToString();
			Log("{0} connected", id);
			clients.Add(id, new Client(client, clientReceived, clientClosed, id));
		}

		private void clientReceived(string msg, string id)
		{
			Log(msg, id);
			Client client = clients[id];

			switch (msg) {
				case "hi":
					client.Send("hey");
					break;
				case "hey":
					client.Send("hi");
					break;
				case "QUIT":
					clientClosed(id);
					break;
				default:
					client.Send(msg);
					break;
			}
		}

		private void clientClosed(string id)
		{
			Log("{0} disconnected", id);
			clients[id].client.Close();
			clients.Remove(id);
		}

		private static void Log(string msg, params object[] args)
		{
			Console.WriteLine(msg, args);
		}
	}
}
