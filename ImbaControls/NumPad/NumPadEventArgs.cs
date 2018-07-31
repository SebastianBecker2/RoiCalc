using System;

namespace ImbaControls.NumPad
{
	public class NumPadEventArgs : EventArgs
	{
		public int Value { get; set; }
		public string Text { get; set; }
		public bool IsDecSign { get; set; }

		public NumPadEventArgs(int value) : this(value, value.ToString(), false)
		{
		}

		public NumPadEventArgs(string text) : this(-1, text, true)
		{
		}

		public NumPadEventArgs(int value, string text, bool is_dec_sign)
		{
			Value = value;
			Text = text;
			IsDecSign = is_dec_sign;
		}
	}
}