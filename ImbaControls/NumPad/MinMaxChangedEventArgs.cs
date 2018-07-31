using System;

namespace ImbaControls.NumPad
{
	public class MinMaxChangedEventArgs : EventArgs
	{
		public long Min { get; set; }
		public long Max { get; set; }

		public MinMaxChangedEventArgs()
		{
		}

		public MinMaxChangedEventArgs(long min, long max)
		{
			Min = min;
			Max = max;
		}
	}
}