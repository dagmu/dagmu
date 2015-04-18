﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DagMUServer
{
	public class Server
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
			LogFormat("Listening on {0} : {1}", ipAddress.ToString(), port);
			while (true) {
				await acceptClient();
			}
		}

		TcpListener listener;
		Dictionary<string, Client> clients = new Dictionary<string, Client>();

		async Task acceptClient()
		{
			var client = await listener.AcceptTcpClientAsync();
			var id = Guid.NewGuid().ToString();
			LogFormat("{0} connected", id);
			clients.Add(id, new Client(client, clientReceived, clientClosed, id));
			clientConnected(clients[id]);
		}

		void clientConnected(Client client)
		{
			string dirFound = findDir("taps");
			if (String.IsNullOrEmpty(dirFound)) return;
			string filename = dirFound + "welcome.txt";

			foreach (var line in File.ReadLines(filename)) {
				client.Send(line);
			}
		}

		void clientReceived(string msg, Client client)
		{
			Log(msg);

			if (msg.StartsWith("dagmuecho ")) {
				var s = msg.Substring("dagmuecho ".Length);
				client.Send(s);
			} else switch (msg) {
				case "exa me=/_page/lastpaged":
					client.Send(new List<string>() {
						"str /_page/lastpaged:Ashkii",
						"1 property listed.",
					});
					break;
				case "exa me=/_whisp/lastwhispered":
					client.Send("0 properties listed.");
					break;
				case "l":
				case "look":
					client.Send(new List<string>() {
						"Name",
						"   Description",
					});
					break;
				case "exa me=/ride/_mode":
					client.Send(new List<string>() {
						"str /RIDE/_mode:hand",
						"1 property listed.",
					});
					break;
				case "wf #hidefrom":
					client.Send("Hiding from: *no one*");
					break;
				case "@mpi {name:me}":
					client.Send(new List<string>() {
						"Arg:    {name:me}",
						"Result: Kael",
					});
					break;
				case "hi":
					client.Send("hey");
					break;
				case "hey":
					client.Send("hi");
					break;
				case "QUIT":
					clientClosed(client);
					break;
				default:
					client.Send(msg);
					break;
			}
		}

		void clientClosed(Client client)
		{
			LogFormat("{0} disconnected", client.id);
			client.client.Close();
			clients.Remove(client.id);
		}

		static void Log(string msg)
		{
			Console.WriteLine(msg);
		}

		static void LogFormat(string msg, params object[] args)
		{
			Console.WriteLine(msg, args);
		}

		private static string findDir(string dir, int levelsToTraverseUp = 4)
		{
			//look up a few directories for taps text folder
			string dirFound = null;
			string directoryPrefix = String.Empty;
			for (int i = 0; i < levelsToTraverseUp; i++) {
				string dirName = (String.IsNullOrEmpty(directoryPrefix) ? "./" : directoryPrefix) + dir;
				if (Directory.Exists(dirName)) {
					dirFound = dirName + "/";
					break;
				}
				directoryPrefix += "../";
			}
			return dirFound;
		}
	}
}
