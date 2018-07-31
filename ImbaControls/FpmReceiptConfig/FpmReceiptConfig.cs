namespace ImbaControls
{
	public partial class FpmReceiptConfig : System.Windows.Forms.UserControl
	{
		private const int DefaultMaxLineCount = 4;
		private const int DefaultRowHeight = 34;
		private const int DefaultMaxLineLength = 23;
		private const string DefaultHeaderTitle = "Header";
		private const string DefaultFooterTitle = "Footer";

		// We use this offset to make the dgv not scroll down under the last
		// row if we tab into the last row.
		private const int DgvHeightOffset = 3;

		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(DefaultMaxLineCount)]
		[System.ComponentModel.Category("Data")]
		[System.ComponentModel.Description("Defines the maximum number of lines per header and per row")]
		public int MaxLineCount { get; set; }

		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(DefaultRowHeight)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Description("Defines the height for each row in the DataGridView")]
		public int RowHeight { get; set; }

		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(DefaultMaxLineLength)]
		[System.ComponentModel.Category("Data")]
		[System.ComponentModel.Description("Defines the maximum number of characters per line")]
		public int MaxLineLength { get; set; }

		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(DefaultHeaderTitle)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Localizable(true)]
		[System.ComponentModel.Description("Defines the text shown as the header title")]
		public string HeaderTitle
		{
			get
			{
				return HeaderLabel.Text;
			}
			set
			{
				HeaderLabel.Text = value;
			}
		}

		[System.ComponentModel.Browsable(true)]
		[System.ComponentModel.DefaultValue(DefaultFooterTitle)]
		[System.ComponentModel.Category("Appearance")]
		[System.ComponentModel.Localizable(true)]
		[System.ComponentModel.Description("Defines the text shown as the footer title")]
		public string FooterTitle
		{
			get
			{
				return FooterLabel.Text;
			}
			set
			{
				FooterLabel.Text = value;
			}
		}

		private System.Collections.Generic.List<string> m_header;

		public System.Collections.Generic.List<string> Header
		{
			get
			{
				return new System.Collections.Generic.List<string>(m_header);
			}
			set
			{
				m_header = value;
				PopulateDgv(dgvHeaders, Header);
			}
		}

		private System.Collections.Generic.List<string> m_footer;

		public System.Collections.Generic.List<string> Footer
		{
			get
			{
				return new System.Collections.Generic.List<string>(m_footer);
			}
			set
			{
				m_footer = value;
				PopulateDgv(dgvFooters, Footer);
			}
		}

		public FpmReceiptConfig()
		{
			Header = new System.Collections.Generic.List<string>();
			Footer = new System.Collections.Generic.List<string>();
			MaxLineCount = DefaultMaxLineCount;
			RowHeight = DefaultRowHeight;
			MaxLineLength = DefaultMaxLineLength;

			InitializeComponent();

			HeaderTitle = DefaultHeaderTitle;
			FooterTitle = DefaultFooterTitle;

			dgvHeaders.Height = 0;
			dgvFooters.Height = 0;

			btnAddHeader.MouseDown += (sender, e) =>
			{
				if (AddRow(dgvHeaders) == -1) return;
				m_header.Add("");
			};
			btnAddFooter.MouseDown += (sender, e) =>
			{
				if (AddRow(dgvFooters) == -1) return;
				m_footer.Add("");
			};

			dgvHeaders.CellValueChanged += (sender, e) =>
			{
				if (e.ColumnIndex != 1) return;
				m_header[e.RowIndex] = (string)dgvHeaders.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			};
			dgvFooters.CellValueChanged += (sender, e) =>
			{
				if (e.ColumnIndex != 1) return;
				m_footer[e.RowIndex] = (string)dgvFooters.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
			};

			dgvHeaders.CellMouseDown += (sender, e) =>
			{
				if (RemoveRow(sender, e))
				{
					m_header.RemoveAt(e.RowIndex);
				}
			};
			dgvFooters.CellMouseDown += (sender, e) =>
			{
				if (RemoveRow(sender, e))
				{
					m_footer.RemoveAt(e.RowIndex);
				}
			};

			dgvHeaders.RowsAdded += DgvRowCountChanged;
			dgvFooters.RowsAdded += DgvRowCountChanged;
			dgvHeaders.RowsRemoved += DgvRowCountChanged;
			dgvFooters.RowsRemoved += DgvRowCountChanged;
		}

		private void PopulateDgv(System.Windows.Forms.DataGridView dgv, System.Collections.Generic.List<string> list)
		{
			foreach (string line in list)
			{
				var row_index = AddRow(dgv);
				if (row_index == -1) break;
				dgv.Rows[row_index].Cells[1].Value = line;
			}
		}

		private void DgvRowCountChanged(object sender, System.EventArgs e)
		{
			var dgv = sender as System.Windows.Forms.DataGridView;
			if (dgv == null) return;

			if (dgv.Rows.Count > 0)
			{
				dgv.Height = DgvHeightOffset + (RowHeight * dgv.Rows.Count);
			}
			else
			{
				dgv.Height = 0;
			}
		}

		private int AddRow(System.Windows.Forms.DataGridView dgv)
		{
			if (dgv == null) return -1;
			if (dgv.RowCount == MaxLineCount) return -1;

			var row = new System.Windows.Forms.DataGridViewRow();
			row.Height = RowHeight;

			var button_cell = new System.Windows.Forms.DataGridViewImageCell();
			button_cell.Value = Properties.Resources.minus;
			row.Cells.Add(button_cell);

			var line_cell = new System.Windows.Forms.DataGridViewTextBoxCell();
			line_cell.MaxInputLength = MaxLineLength;
			row.Cells.Add(line_cell);

			return dgv.Rows.Add(row);
		}

		private bool RemoveRow(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
		{
			if (e.ColumnIndex > 0) return false;

			var dgv = sender as System.Windows.Forms.DataGridView;
			if (dgv == null) return false;

			dgv.Rows.RemoveAt(e.RowIndex);
			return true;
		}
	}
}