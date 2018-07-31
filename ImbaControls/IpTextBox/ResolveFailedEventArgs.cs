using System;

namespace ImbaControls.IpTextBox
{
	public class ResolveFailedEventArgs : EventArgs
	{
		public string Text;

		public ResolveFailedEventArgs(string text)
		{
			Text = text;
		}
	}
}