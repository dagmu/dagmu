using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DagMU.Model
{
	public static class Echo
	{
		/// <param name="s">text from connection.</param>
		/// <param name="msg">Will be the contained message, if echo was valid.</param>
		/// <returns>True if valid.</returns>
		public static bool isEcho(string s, out string msg, string sessionGuid)
		{
			if (String.IsNullOrEmpty(s)) throw new ArgumentNullException("msg");

			if (s.StartsWith(DagMU.Model.Constants.dagmu_echo_prefix)) {
				string remainder = s.Substring(DagMU.Model.Constants.dagmu_echo_prefix.Length);
				string guid = remainder.Substring(0, sessionGuid.Length);
				if (guid == sessionGuid) {
					msg = remainder.Substring(sessionGuid.Length).Trim();
					return true;
				}
			}

			msg = null;
			return false;
		}

		/// <param name="s">text from connection.</param>
		/// <param name="message">message to match against, if its a valid echo.</param>
		/// <returns>true if a valid echo matched supplied message.</returns>
		public static bool isEchoEqual(string s, string message, string sessionGuid)
		{
			if (String.IsNullOrEmpty(s)) return false;

			string echoMessage;
			if (isEcho(s, out echoMessage, sessionGuid)) {
				if (message == echoMessage) {
					return true;
				}
			}
			return false;
		}
	}
}
