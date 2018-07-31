using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ImbaControls.NumPad
{
	public class TouchLabel : Label
	{
		public readonly static Color DefaultEnabledBackColor = Color.YellowGreen;
		public readonly static Color DefaultDisabledBackColor = Color.Khaki;
		public readonly static BorderStyle DefaultEnabledBorderStyle = BorderStyle.Fixed3D;
		public readonly static BorderStyle DefaultDisabledBorderStyle = BorderStyle.None;

		protected override void InitLayout()
		{
			base.InitLayout();
			base.AutoSize = false;
			base.TextAlign = ContentAlignment.MiddleRight;
			base.BackColor = Color.YellowGreen;
			base.BorderStyle = BorderStyle.Fixed3D;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected new bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		protected new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		public void SetEnabled()
		{
			base.BackColor = DefaultEnabledBackColor;
			base.BorderStyle = DefaultEnabledBorderStyle;
			Focus();
		}

		public void SetDisabled()
		{
			base.BackColor = DefaultDisabledBackColor;
			base.BorderStyle = DefaultDisabledBorderStyle;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(true)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				if (value) SetEnabled(); else SetDisabled();
			}
		}

		public virtual void NumPadClickEvent(object sender, NumPadEventArgs e)
		{
			AddString(e.Text);
		}

		public virtual void KeyHandler(object sender, KeyEventArgs e)
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
			else if ((e.KeyCode >= Keys.A) && (e.KeyCode <= Keys.Z))
			{
				var kc = new System.Windows.Forms.KeysConverter();
				if (!e.Shift)
				{
					AddString(kc.ConvertToString(e.KeyCode).ToLower());
				}
				else
				{
					AddString(kc.ConvertToString(e.KeyCode));
				}
			}
			else if ((e.KeyCode >= Keys.D0) && (e.KeyCode <= Keys.D9))
			{
				AddString((e.KeyCode - Keys.D0).ToString());
			}
			else if ((e.KeyCode >= Keys.NumPad0) && (e.KeyCode <= Keys.NumPad9))
			{
				AddString((e.KeyCode - Keys.NumPad0).ToString());
			}
			else if (e.KeyCode == Keys.Space)
			{
				AddString(" ");
			}
		}

		public virtual void AddString(string value)
		{
			base.Text += value;
		}

		public virtual void Clear()
		{
			base.Text = "";
		}

		public virtual void Delete()
		{
			base.Text = base.Text.RemoveLastCharacter();
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			this.ResumeLayout(false);
		}
	}
}