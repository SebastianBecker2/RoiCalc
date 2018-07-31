using System;
using System.Globalization;
using System.Windows.Forms;

namespace ImbaControls.NumPad
{
	public class CurrencyBox : TextBox
	{
		protected NumPadLabel NumValue { get; set; }

		public long Value
		{
			get { return NumValue.Value; }
			set { NumValue.Value = value; }
		}

		protected NumberFormatInfo FormatInfo { get; set; }

		public CurrencyBox()
		{
			FormatInfo = CultureInfo.CurrentCulture.NumberFormat;
			NumValue = new NumPadLabel();
			NumValue.TextChanged += NumValue_TextChanged;
			NumValue.Format = @"100C";
			NumValue.Text = @"0,00 €";
			NumValue.SetMinMax(0, uint.MaxValue);
			TextAlign = HorizontalAlignment.Right;
		}

		private void NumValue_TextChanged(object sender, EventArgs args)
		{
			Text = NumValue.Text;
		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			e.Handled = true;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			NumValue.KeyHandler(this, e);
			e.Handled = true;
		}

		protected override void OnGotFocus(EventArgs e)
		{
			SetSelection();
			base.OnGotFocus(e);
		}

		protected override void OnTextChanged(EventArgs e)
		{
			SetSelection();
			base.OnTextChanged(e);
		}

		protected void SetSelection()
		{
			SelectionLength = 0;
			SelectionStart = Text.Length;
		}
	}
}