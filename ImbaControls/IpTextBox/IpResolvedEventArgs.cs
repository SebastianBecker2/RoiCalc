using System;
using System.Net;

namespace ImbaControls.IpTextBox
{
	public class IpResolvedEventArgs : EventArgs
	{
		public IPAddress IpAddress;

		public IpResolvedEventArgs(IPAddress ip_address)
		{
			IpAddress = ip_address;
		}
	}
}