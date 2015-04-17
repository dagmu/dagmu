using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace DagMUServer
{
	class Server
	{
		TcpListener listener;
		Dictionary<string, Client> clients = new Dictionary<string, Client>();

		public async void Start(IPAddress ipAddress, int port)
		{
			listener = new TcpListener(ipAddress, port);
			listener.Start();
			while (true) {
				var client = await listener.AcceptTcpClientAsync();
				var id = Guid.NewGuid().ToString();
				clients.Add(id, new Client(client, clientReceived, clientClosed, id));
			}
		}

		public void clientReceived(string msg, string id)
		{
			Console.WriteLine(msg);
			Client client = clients[id];
			switch (msg) {
				case "hi":
					client.Send("hey");
					break;
				case "hey":
					client.Send("hi");
					break;
			}
		}

		public void clientClosed(string id)
		{
			Console.WriteLine(String.Format("Client {0} disconnected.", id));
			clients.Remove(id);
		}
	}
}
