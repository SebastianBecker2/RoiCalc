using System;

namespace ImbaControls.NumPad
{
	public class NumPadValue : INumPadValue
	{
		private bool m_reset = true;
		private bool m_decimal_set;

		public long Value
		{
			get { return m_value; }
			set
			{
				var buffer = m_value;
				m_value = Math.Max(Math.Min(value, MaxValue), MinValue);
				if (buffer != m_value)
				{
					OnValueChanged(m_value);
				}
			}
		}

		private long m_value;

		public long MaxValue
		{
			get { return m_max_value; }
			set
			{
				m_max_value = Math.Max(value, MinValue);
				Value = Value;
			}
		}

		private long m_max_value = long.MaxValue;

		public long MinValue
		{
			get { return m_min_value; }
			set
			{
				m_min_value = value;
				Value = Value;
			}
		}

		private long m_min_value = long.MinValue;

		public event EventHandler<ValueChangedEventArgs> ValueChanged;

		protected virtual void OnValueChanged(ValueChangedEventArgs args)
		{
			ValueChanged?.Invoke(this, args);
		}

		protected virtual void OnValueChanged(long value)
		{
			OnValueChanged(new ValueChangedEventArgs(value));
		}

		public event EventHandler<MinMaxChangedEventArgs> MinMaxChanged;

		protected void OnMinMaxChanged(MinMaxChangedEventArgs args)
		{
			MinMaxChanged?.Invoke(this, args);
		}

		protected void OnMinMaxChanged(long min, long max)
		{
			OnMinMaxChanged(new MinMaxChangedEventArgs(min, max));
		}

		public NumPadValue()
		{
		}

		public NumPadValue(long min, long max)
		{
			SetMinMax(min, max);
		}

		public void SetMinMax(long min, long max)
		{
			MinValue = min;
			MaxValue = max;
		}

		public long Add(long value)
		{
			var current_value = Value;
			var current_reset = m_reset;
			var successful = true;

			try
			{
				if (value < 10)
				{
					if (current_reset)
					{
						current_value = 0;
						current_reset = false;
					}

					current_value *= 10;
					current_value += value;
				}
				else
				{
					current_value += (value * 100);
					current_reset = true;
				}
			}
			catch
			{
				successful = false;
			}

			if (successful)
			{
				m_reset = current_reset;
				Value = current_value;
			}

			return current_value;
		}

		public long Increment(int value)
		{
			var current_value = Value;
			var current_reset = m_reset;
			var successful = true;

			try
			{
				current_value += value;
				current_reset = true;
			}
			catch
			{
				successful = false;
			}

			if (successful)
			{
				m_reset = current_reset;
				Value = current_value;
			}

			return Value;
		}

		public long SetDecimal()
		{
			var current_value = Value;
			var successful = true;

			try
			{
				if ((!m_decimal_set) && (current_value != 0))
				{
					current_value += 10;
					m_decimal_set = true;
				}
			}
			catch
			{
				successful = false;
			}

			if (successful)
			{
				Value = current_value;
			}

			return Value;
		}

		public long Delete()
		{
			var current_value = Value;
			var current_reset = m_reset;
			var successful = true;

			try
			{
				if (current_value > 10)
				{
					current_value = current_value / 10;
					current_reset = false;
				}
				else
				{
					current_value = 0;
					current_reset = true;
				}
			}
			catch
			{
				successful = false;
			}

			if (successful)
			{
				m_reset = current_reset;
				Value = current_value;
			}

			return Value;
		}

		public long SetToMax()
		{
			Value = MaxValue;
			return Value;
		}

		public long SetToMin()
		{
			Value = MinValue;
			return Value;
		}

		public void Clear()
		{
			Value = MinValue;
			m_reset = true;
			m_decimal_set = false;
		}
	}
}