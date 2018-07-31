using System.Net;

namespace ImbaControls.IpTextBox
{
	public class NameResolvedEventArgs : IpResolvedEventArgs
	{
		public string Name;

		public NameResolvedEventArgs(IPAddress ip_address, string name)
			: base(ip_address)
		{
			Name = name;
		}
	}
}