using System;
using System.Linq;
using System.Windows.Forms;

namespace ImbaControls.NumPad
{
	public class IpLabel : TouchLabel, IIncrementableValue
	{
		private bool m_overwrite_ip = true;

		public bool Valid { get; set; }

		public delegate void ValidationChangedDelegate(object sender, bool e);

		public event ValidationChangedDelegate ValidationChanged;

		public IpLabel()
		{
			Valid = false;
			TextChanged += (sender, e) => { Validate(); };
		}

		public override void NumPadClickEvent(object sender, NumPadEventArgs e)
		{
			if (e.IsDecSign)
			{
				AddDot();
			}
			else
			{
				AddNumber(e.Value);
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
			else if ((e.KeyCode == Keys.Decimal) || (e.KeyCode == Keys.OemPeriod) || (e.KeyCode == Keys.Oemcomma))
			{
				AddDot();
			}
			else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9))
			{
				AddNumber(Math.Min(e.KeyCode - Keys.D0, 9));
			}
			else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9))
			{
				AddNumber(Math.Min(e.KeyCode - Keys.NumPad0, 9));
			}
			else if ((e.KeyCode == Keys.Add) || (e.KeyCode == Keys.Up))
			{
				SetLastOctetValue(GetLastOctetValue() + 1);
			}
			else if ((e.KeyCode == Keys.Subtract) || (e.KeyCode == Keys.Down))
			{
				SetLastOctetValue(GetLastOctetValue() - 1);
			}
			else if (e.KeyCode == Keys.PageUp)
			{
				SetLastOctetValue(GetLastOctetValue() + 5);
			}
			else if (e.KeyCode == Keys.PageDown)
			{
				SetLastOctetValue(GetLastOctetValue() - 5);
			}
			else if (e.KeyCode == Keys.Right)
			{
				SetLastOctetValue(GetLastOctetValue() + 10);
			}
			else if (e.KeyCode == Keys.Left)
			{
				SetLastOctetValue(GetLastOctetValue() - 10);
			}
			else if (e.KeyCode == Keys.Home)
			{
				SetLastOctetValue(0);
			}
			else if (e.KeyCode == Keys.End)
			{
				SetLastOctetValue(255);
			}
		}

		public void AddDot()
		{
			if (m_overwrite_ip)
			{
				base.Text = "";
				m_overwrite_ip = false;
			}

			if ((GetOctetCount() < 3) && (!(GetLastOctet() == "")))
			{
				base.Text += '.';
			}
		}

		public void AddNumber(int value)
		{
			AddNumber(value.ToString().First());
		}

		public void AddNumber(char value)
		{
			if (m_overwrite_ip)
			{
				base.Text = "";
				m_overwrite_ip = false;
			}

			if (CheckOctet(GetLastOctet() + value))
			{
				base.Text += value;
			}
			if (!CheckOctet(GetLastOctet() + "0"))
			{
				if (GetOctetCount() < 3)
				{
					base.Text += '.';
				}
			}
		}

		long IIncrementableValue.Increment(int value)
		{
			return Increment(value);
		}

		public int Increment(int value)
		{
			int octet = GetLastOctetValue() + value;
			SetLastOctetValue(octet);
			return octet;
		}

		private string GetLastOctet()
		{
			var last_dot = base.Text.LastIndexOf('.');
			if (last_dot == -1) return base.Text;
			if (base.Text.Length <= last_dot + 1) return "";

			return base.Text.Substring((last_dot + 1), (base.Text.Length - (last_dot + 1)));
		}

		private static bool CheckOctet(string octet)
		{
			if (octet.Length > 3) return false;
			var value = octet.SoftParse(-1);
			if ((value < 0) || (value > 255)) return false;
			return true;
		}

		private int GetLastOctetValue()
		{
			return GetLastOctet().SoftParse(0);
		}

		private void SetLastOctetValue(int octet)
		{
			SetLastOctet(octet.ToString());
		}

		private void SetLastOctet(string octet)
		{
			if (!CheckOctet(octet)) return;

			var last_dot = base.Text.LastIndexOf('.');
			if (last_dot == -1) base.Text = octet;
			base.Text = base.Text.Substring(0, (last_dot + 1)) + octet;
		}

		private int GetOctetCount()
		{
			return base.Text.Count((c) => { return (c == '.'); });
		}

		private bool Validate()
		{
			var address = new System.Net.IPAddress(new Byte[] { 127, 0, 0, 1 });
			if (!System.Net.IPAddress.TryParse(base.Text, out address))
			{
				if (Valid)
				{
					Valid = !Valid;
					ValidationChanged(this, Valid);
				}
				return false;
			}
			if (!Valid)
			{
				Valid = !Valid;
				ValidationChanged(this, Valid);
			}
			return true;
		}

		public override void Delete()
		{
			if (!CheckOctet(GetLastOctet()))
			{
				base.Delete();
			}
			base.Delete();
			m_overwrite_ip = false;
		}
	}
}