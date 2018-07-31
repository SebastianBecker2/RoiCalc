using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ImbaControls.NumPad
{
	public class NumPadLabel : TouchLabel, INumPadValue
	{
		protected NumPadValue m_value = new NumPadValue();

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[Category("Data")]
		[Description("Gets and sets the value displayed by the control.")]
		public long Value { get { return m_value.Value; } set { m_value.Value = value; } }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[DefaultValue("D")]
		[Category("Appearance")]
		[Description("Defines the format with an optional integer followed by" +
			" a Standard Numeric Format String. The leading integer, if present," +
			" will be used as a divisor for the value before applying the format.")]
		public string Format { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[DefaultValue("")]
		[Category("Appearance")]
		[Description("Defines string to be displayed as a prefix of the value." +
			"This can be used as a unit text if the numeric format doesn't include one.")]
		public string UnitText { get; set; }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[Category("Data")]
		[Description("Defines the maximum value allowed for the control.")]
		public long MaxValue { get { return m_value.MaxValue; } set { m_value.MaxValue = value; } }

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[Browsable(true)]
		[Category("Data")]
		[Description("Defines the minimum value allowed for the control.")]
		public long MinValue { get { return m_value.MinValue; } set { m_value.MinValue = value; } }

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

		protected virtual void OnMinMaxChanged(long min, long max)
		{
			OnMinMaxChanged(new MinMaxChangedEventArgs(min, max));
		}

		public NumPadLabel()
		{
			Format = "D";
			UnitText = "";
			m_value.ValueChanged += (s, args) =>
			{
				FormatText(args.Value);
				OnValueChanged(args);
			};
			m_value.MinMaxChanged += (s, args) =>
			{
				OnMinMaxChanged(args);
			};
		}

		protected virtual void FormatText(long value)
		{
			var format_char_index = Format.IndexOfAny("CcDdEeFfGgNnPpRrXx".ToCharArray());
			if (format_char_index == -1) throw new FormatException("Format specifier was invalid.");

			int prefix = 0;
			if (format_char_index > 0) prefix = Format.Substring(0, format_char_index).SoftParse(-1);
			if (prefix <= -1) throw new FormatException("Format specifier was invalid.");

			if (prefix > 0)
			{
				base.Text = ((float)value / prefix).ToString(Format.Substring(format_char_index));
			}
			else
			{
				base.Text = value.ToString(Format.Substring(format_char_index));
			}
			if (!string.IsNullOrWhiteSpace(UnitText))
			{
				base.Text += " " + UnitText;
			}
		}

		protected override void InitLayout()
		{
			base.InitLayout();
		}

		public override void NumPadClickEvent(object sender, NumPadEventArgs e)
		{
			if (e.IsDecSign)
			{
				m_value.SetDecimal();
			}
			else
			{
				m_value.Add(e.Value);
			}
		}

		public override void KeyHandler(object sender, KeyEventArgs e)
		{
			// C# can not work with ranges in switch case, so we use if-elseif
			if (e.KeyCode == Keys.Delete)
			{
				Clear();
			}
			else if (e.KeyCode == Keys.Back)
			{
				Delete();
			}
			else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9))
			{
				Add((long)(e.KeyValue - Keys.D0));
			}
			else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9))
			{
				Add((long)(e.KeyValue - Keys.NumPad0));
			}
			else if ((e.KeyCode == Keys.Add) || (e.KeyCode == Keys.Up))
			{
				m_value.Increment(1);
			}
			else if ((e.KeyCode == Keys.Subtract) || (e.KeyCode == Keys.Down))
			{
				m_value.Increment(-1);
			}
			else if (e.KeyCode == Keys.PageUp)
			{
				m_value.Increment(5);
			}
			else if (e.KeyCode == Keys.PageDown)
			{
				m_value.Increment(-5);
			}
			else if (e.KeyCode == Keys.Right)
			{
				m_value.Increment(10);
			}
			else if (e.KeyCode == Keys.Left)
			{
				m_value.Increment(-10);
			}
			else if (e.KeyCode == Keys.Home)
			{
				m_value.SetToMin();
			}
			else if (e.KeyCode == Keys.End)
			{
				m_value.SetToMax();
			}
		}

		// Hide AddString(string) since we don't wanna use it here
		private new void AddString(string value)
		{
			return;
		}

		public override void Clear()
		{
			m_value.Clear();
		}

		public override void Delete()
		{
			m_value.Delete();
		}

		long INumPadValue.Delete()
		{
			return m_value.Delete();
		}

		public long Increment(int value)
		{
			return m_value.Increment(value);
		}

		public long SetDecimal()
		{
			return m_value.SetDecimal();
		}

		public long SetToMin()
		{
			return m_value.SetToMin();
		}

		public long SetToMax()
		{
			return m_value.SetToMax();
		}

		public long Add(long value)
		{
			return m_value.Add(value);
		}

		public void SetMinMax(long min_value, long max_value)
		{
			m_value.SetMinMax(min_value, max_value);
		}
	}
}