using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ImbaControls.NumPad
{
	public partial class NumPad : UserControl, INumPadValue
	{
		private NumPadValue m_value = new NumPadValue();

		[Browsable(false)]
		public long Value
		{
			set { m_value.Value = value; }
			get { return m_value.Value; }
		}

		public long MaxValue
		{
			set { m_value.MaxValue = value; }
			get { return m_value.MaxValue; }
		}

		public long MinValue
		{
			get { return m_value.MinValue; }
			set { m_value.MinValue = value; }
		}

		public bool DecimalVisible
		{
			get { return btnDecSign.Visible; }
			set { btnDecSign.Visible = value; }
		}

		public new event EventHandler<NumPadEventArgs> Click;

		protected virtual void OnClick(NumPadEventArgs args)
		{
			Click?.Invoke(this, args);
		}

		protected virtual void OnClick(string text)
		{
			OnClick(new NumPadEventArgs(text));
		}

		protected virtual void OnClick(int value)
		{
			OnClick(new NumPadEventArgs(value));
		}

		public new event EventHandler<NumPadEventArgs> KeyDown;

		protected virtual void OnKeyDown(NumPadEventArgs args)
		{
			KeyDown?.Invoke(this, args);
		}

		protected virtual void OnKeyDown(string text)
		{
			OnKeyDown(new NumPadEventArgs(text));
		}

		protected virtual void OnKeyDown(int value)
		{
			OnKeyDown(new NumPadEventArgs(value));
		}

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

		protected virtual void OnMinMaxChanged(MinMaxChangedEventArgs args)
		{
			MinMaxChanged?.Invoke(this, args);
		}

		public NumPad()
		{
			InitializeComponent();

			btnDecSign.Text = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

			m_value.ValueChanged += (sender, args) =>
			{
				OnValueChanged(args);
			};

			m_value.MinMaxChanged += (sender, args) =>
			{
				OnMinMaxChanged(args);
			};

			Click += (sender, args) =>
			{
				if (args.IsDecSign)
				{
					SetDecimal();
				}
				else
				{
					Add(args.Value);
				}
			};
		}

		private void AnyButtonClicked(object sender, EventArgs args)
		{
			var btn_text = (sender as Button).Text;
			var btn_value = 0;

			if (int.TryParse(btn_text, out btn_value))
			{
				OnClick(btn_value);
			}
			else
			{
				OnClick(btn_text);
			}
		}

		public void KeyDownButtonHandler(object sender, KeyEventArgs args)
		{
			KeyDownHandler(sender, args);
		}

		public bool KeyDownHandler(object sender, KeyEventArgs args)
		{
			switch (args.KeyCode)
			{
			case Keys.OemPeriod:
			case Keys.Decimal:
			case Keys.Oemcomma:
				OnKeyDown(btnDecSign.Text);
				break;

			case Keys.NumPad0:
			case Keys.NumPad1:
			case Keys.NumPad2:
			case Keys.NumPad3:
			case Keys.NumPad4:
			case Keys.NumPad5:
			case Keys.NumPad6:
			case Keys.NumPad7:
			case Keys.NumPad8:
			case Keys.NumPad9:
				OnKeyDown((int)args.KeyCode - (int)Keys.NumPad0);
				break;

			case Keys.D0:
			case Keys.D1:
			case Keys.D2:
			case Keys.D3:
			case Keys.D4:
			case Keys.D5:
			case Keys.D6:
			case Keys.D7:
			case Keys.D8:
			case Keys.D9:
				OnKeyDown((int)args.KeyCode - (int)Keys.D0);
				break;

			default:
				return false;
			}
			return true;
		}

		public void SetNumPadValue(NumPadValue value)
		{
			m_value = value;
		}

		public void SetMinMax(long min, long max)
		{
			MinValue = min;
			MaxValue = max;
		}

		public long Add(long value)
		{
			return m_value.Add(value);
		}

		public long Increment(int value)
		{
			return m_value.Increment(value);
		}

		public long SetDecimal()
		{
			return m_value.SetDecimal();
		}

		public long Delete()
		{
			return m_value.Delete();
		}

		public long SetToMax()
		{
			return m_value.SetToMax();
		}

		public long SetToMin()
		{
			return m_value.SetToMin();
		}

		public void Clear()
		{
			m_value.Clear();
		}
	}
}