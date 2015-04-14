using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Text;

namespace DagMU.Forms
{
	partial class MuckConnection
	{
		TcpClient tcp = null;
		string address;
		int port;
		StreamWriter writer;

		/// <summary>
		/// Is this currently connected to a server? Readonly.
		/// </summary>
		public bool Connected { get { return tcp == null ? false : tcp.Connected; } }

		/// <summary>
		/// Connection status update: Connect error, connect success, disconnected
		/// </summary>
		public event EventHandler<ConnectEventArgs> EConnect;

		/// <summary>
		/// Receive line from muck
		/// </summary>
		public event EventHandler<string> ERead;

		/// <summary>
		/// Sent line or send error
		/// object inputbox, SendStatus status, string errarMessage
		/// </summary>
		public event EventHandler<Tuple<SendStatus, string>> ESend;

		/// <summary>
		/// Attempt to connect to the server.
		/// EConnect returns success/fail.
		/// ERead returns muck lines as they arrive.
		/// </summary>
		/// <param name="address">Address of the server, can be an url or ip number</param>
		/// <param name="port">Port of server, an integer between 1 and 65535</param>
		public async Task Connect(string address, int port, bool ssl, string remoteCertificateHash = null)
		{
			this.address = address;
			this.port = port;
			if (port < 1 || port > 65535)
				throw new ArgumentOutOfRangeException("port");

			//connect
			try {
				tcp = new TcpClient(address, port);
			} catch (Exception e) {
				EConnect(null, new ConnectEventArgs(ConnectEventArgs.StatusEnum.Error_Connecting, e.Message));
				return;
			}

			//connect
			StreamReader reader = null;
			try {
				Stream stream = tcp.GetStream();
				if (ssl) {
					SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback((s, cert, ch, er) => {
						var hashString = cert.GetCertHashString();
						if (remoteCertificateHash == null) {//no trusted certs, need to prompt user
							return false;//deny for now
						} else {
							if (hashString == remoteCertificateHash) return true;
						}
						return false;
					}));
					stream = sslStream;
					sslStream.AuthenticateAsClient(address);
					X509Certificate rc = sslStream.RemoteCertificate;
				}
				reader = new StreamReader(stream, Encoding.ASCII);
				writer = new StreamWriter(stream, Encoding.ASCII) { AutoFlush = true };
			} catch (Exception e) {
				EConnect(null, new ConnectEventArgs(ConnectEventArgs.StatusEnum.Error_Connecting, e.Message));
				return;
			}
			EConnect(null, new ConnectEventArgs(ConnectEventArgs.StatusEnum.Connected, null));

			//read until disconnected
			while (true) {
				string line = null;
				try {
					line = await reader.ReadLineAsync();
				} catch (Exception e) {
					Disconnect(ConnectEventArgs.StatusEnum.Error_Connecting, e.Message);
					break;
				}
				ERead(null, line);
			}
		}

		public void Disconnect(ConnectEventArgs.StatusEnum? reason = null, string message = null)
		{
			if (tcp != null && tcp.Connected) tcp.Close();

			EConnect(null, new ConnectEventArgs(reason ?? ConnectEventArgs.StatusEnum.Got_Disconnected, message ?? "Got disconnected."));
		}

		/// <summary>
		/// Send a single line of text to the server.
		/// </summary>
		/// <param name="line">Text to send.</param>
		/// <param name="sender">Optional object ref that will be returned to the parent's OnSend</param>
		public void Send(String line, Object sender = null)
		{
			if (String.IsNullOrEmpty(line)) return;//can't send an empty line
			if (!Connected) return;//not connected

			try {
				writer.WriteLine(line);
				ESend(sender, new Tuple<SendStatus,string>(SendStatus.sent, null));
			} catch (Exception e) {
				ESend(null, new Tuple<SendStatus,string>(SendStatus.send_error, e.Message));
				Disconnect();
			}
		}

		public class ConnectEventArgs : EventArgs
		{
			public ConnectEventArgs(StatusEnum status, string message) {
				this.Status = status;
				this.Message = message;
			}
			public StatusEnum Status;
			public string Message;

			public enum StatusEnum {
				Error_Connecting,
				Connected,
				Got_Disconnected
			}
		}

		public enum SendStatus
		{
			sent,		// message was sent, no problem
			send_error	// an error occured and message wasn't sent
		}
	}
}
