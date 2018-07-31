using System;

namespace ImbaControls.NumPad
{
	public class ValueChangedEventArgs : EventArgs
	{
		public long Value { get; set; }

		public ValueChangedEventArgs()
		{
		}

		public ValueChangedEventArgs(long value)
		{
			Value = value;
		}
	}
}