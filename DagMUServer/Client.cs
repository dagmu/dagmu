using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Client
	{
		private readonly TcpClient client;

		private readonly Action<string> callbackClosed;
		private readonly Action<string, string> callbackReceive;

		private readonly StreamReader reader;
		private readonly StreamWriter writer;

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
				var message = await reader.ReadLineAsync();

				if (String.IsNullOrEmpty(message)) {
					await Task.Run(() => callbackClosed(id));
					return;
				}

				callbackReceive(message, id);
			}
		}

		public async void Send(string message)
		{
			if (String.IsNullOrEmpty(message)) return;

			await writer.WriteLineAsync(message);
		}
	}
}
