using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace DagMUServer
{
	class Client
	{
		internal readonly TcpClient client;

		readonly Action<Client> callbackClosed;
		readonly Action<string, Client> callbackReceive;
		readonly StreamReader reader;
		readonly StreamWriter writer;

		internal readonly string id;

		internal Client(TcpClient client, Action<string, Client> callbackReceive, Action<Client> callbackClose, string id)
		{
			this.callbackClosed = callbackClose;
			this.client = client;
			this.callbackReceive = callbackReceive;
			this.id = id;

			this.reader = new StreamReader(client.GetStream());
			this.writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

			StartReceive();
		}

		internal void Send(IEnumerable<string> lines)
		{
			foreach (var line in lines) Send(line);
		}

		internal async void Send(string message)
		{
			if (String.IsNullOrEmpty(message)) return;

			await sendLock.WaitAsync();
			try {
				await writer.WriteLineAsync(message);
			} finally {
				sendLock.Release();
			}
		}
		private static SemaphoreSlim sendLock = new SemaphoreSlim(1);

		async void StartReceive()
		{
			while (true) {
				string line;
				try {
					line = await reader.ReadLineAsync();
				} catch { return; }

				if (String.IsNullOrEmpty(line)) {
					callbackClosed(this);
					return;
				}

				callbackReceive(line, this);
			}
		}
	}
}
