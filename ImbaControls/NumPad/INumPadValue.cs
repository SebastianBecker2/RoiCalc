using System;

namespace ImbaControls.NumPad
{
	public interface INumPadValue : IIncrementableValue
	{
		long Value { get; set; }
		long MaxValue { get; set; }
		long MinValue { get; set; }

		event EventHandler<MinMaxChangedEventArgs> MinMaxChanged;

		event EventHandler<ValueChangedEventArgs> ValueChanged;

		void SetMinMax(long min_value, long max_value);

		long Add(long value);

		long Delete();

		long SetToMin();

		long SetToMax();
	}
}