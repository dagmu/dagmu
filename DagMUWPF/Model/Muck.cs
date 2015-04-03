using System;

namespace DagMUWPF.Model
{
	[Serializable]
	public class Muck
	{
		public String Name { get; set; }
		public String NameShort { get; set; }
		public uint Port { get; set; }
		public String Address { get; set; }
		public uint SendBufferSize { get; set; }
		public string QUITString { get; set; }
	}
}
