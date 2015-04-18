using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Server
	{
		TcpListener listener;
		Dictionary<string, Client> clients = new Dictionary<string, Client>();

		public async void Start(IPAddress ipAddress, int port)
		{
			Log(typeof(Program).Assembly.GetName().Name);
			listener = new TcpListener(port);
			listener.Start();
			Log("Listening on {0} : {1}", ipAddress.ToString(), port);
			while (true) {
				await acceptClient();
			}
		}

		private async Task acceptClient()
		{
			var client = await listener.AcceptTcpClientAsync();
			var id = Guid.NewGuid().ToString();
			Log("Accepted client {0}.", id);
			clients.Add(id, new Client(client, clientReceived, clientClosed, id));
		}

		public void clientReceived(string msg, string id)
		{
			Log(msg);
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

		public void clientClosed(string id)
		{
			Log("Client {0} disconnected.", id);
			clients[id].client.Close();
			clients.Remove(id);
		}

		public static void Log(string msg, params object[] args)
		{
			Console.WriteLine(msg, args);
		}
	}
}
