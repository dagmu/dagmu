using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Client
	{
		internal readonly TcpClient client;

		readonly Action<string> callbackClosed;
		readonly Action<string, string> callbackReceive;
		readonly StreamReader reader;
		readonly StreamWriter writer;

		private readonly string id;
		public string Id { get { return id; } }

		public Client(TcpClient client, Action<string, string> callbackReceive, Action<string> callbackClose, string id)
		{
			this.callbackClosed = callbackClose;
			this.client = client;
			this.callbackReceive = callbackReceive;
			this.id = id;

			this.reader = new StreamReader(client.GetStream());
			this.writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

			StartReceive();
		}

		public async void StartReceive()
		{
			while (true) {
				string line; 
				try {
					line = await reader.ReadLineAsync();
				} catch (ObjectDisposedException) { return; }

				if (String.IsNullOrEmpty(line)) {
					callbackClosed(id);
					return;
				}

				callbackReceive(line, id);
			}
		}

		public async void Send(string message)
		{
			if (String.IsNullOrEmpty(message)) return;

			await writer.WriteLineAsync(message);
		}
	}
}
