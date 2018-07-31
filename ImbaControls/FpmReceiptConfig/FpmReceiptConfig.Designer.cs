namespace ImbaControls
{
	partial class FpmReceiptConfig {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
      this.TableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.HeaderLabel = new System.Windows.Forms.Label();
      this.dgvFooters = new System.Windows.Forms.DataGridView();
      this.DataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
      this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.FooterLabel = new System.Windows.Forms.Label();
      this.dgvHeaders = new System.Windows.Forms.DataGridView();
      this.AddButtonColumn = new System.Windows.Forms.DataGridViewImageColumn();
      this.LineTextColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.btnAddHeader = new System.Windows.Forms.Button();
      this.btnAddFooter = new System.Windows.Forms.Button();
      this.TableLayoutPanel1.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvFooters)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvHeaders)).BeginInit();
      this.SuspendLayout();
      // 
      // TableLayoutPanel1
      // 
      this.TableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      this.TableLayoutPanel1.ColumnCount = 4;
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 45F));
      this.TableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
      this.TableLayoutPanel1.Controls.Add(this.HeaderLabel, 1, 1);
      this.TableLayoutPanel1.Controls.Add(this.dgvFooters, 2, 2);
      this.TableLayoutPanel1.Controls.Add(this.FooterLabel, 3, 1);
      this.TableLayoutPanel1.Controls.Add(this.dgvHeaders, 0, 2);
      this.TableLayoutPanel1.Controls.Add(this.btnAddHeader, 0, 0);
      this.TableLayoutPanel1.Controls.Add(this.btnAddFooter, 2, 0);
      this.TableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.TableLayoutPanel1.Name = "TableLayoutPanel1";
      this.TableLayoutPanel1.RowCount = 3;
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.TableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.TableLayoutPanel1.Size = new System.Drawing.Size(807, 248);
      this.TableLayoutPanel1.TabIndex = 10;
      // 
      // HeaderLabel
      // 
      this.HeaderLabel.AutoSize = true;
      this.HeaderLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.HeaderLabel.Location = new System.Drawing.Point(48, 10);
      this.HeaderLabel.Name = "HeaderLabel";
      this.HeaderLabel.Size = new System.Drawing.Size(45, 13);
      this.HeaderLabel.TabIndex = 0;
      this.HeaderLabel.Text = "Header:";
      this.HeaderLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // dgvFooters
      // 
      this.dgvFooters.AllowUserToAddRows = false;
      this.dgvFooters.AllowUserToDeleteRows = false;
      this.dgvFooters.AllowUserToResizeColumns = false;
      this.dgvFooters.AllowUserToResizeRows = false;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvFooters.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
      this.dgvFooters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgvFooters.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvFooters.ColumnHeadersVisible = false;
      this.dgvFooters.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DataGridViewImageColumn1,
            this.DataGridViewTextBoxColumn1});
      this.TableLayoutPanel1.SetColumnSpan(this.dgvFooters, 2);
      dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
      dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvFooters.DefaultCellStyle = dataGridViewCellStyle2;
      this.dgvFooters.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dgvFooters.Location = new System.Drawing.Point(406, 48);
      this.dgvFooters.MultiSelect = false;
      this.dgvFooters.Name = "dgvFooters";
      this.dgvFooters.RowHeadersVisible = false;
      dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvFooters.RowsDefaultCellStyle = dataGridViewCellStyle3;
      this.dgvFooters.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvFooters.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.dgvFooters.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgvFooters.ShowCellErrors = false;
      this.dgvFooters.ShowEditingIcon = false;
      this.dgvFooters.ShowRowErrors = false;
      this.dgvFooters.Size = new System.Drawing.Size(398, 197);
      this.dgvFooters.TabIndex = 8;
      // 
      // DataGridViewImageColumn1
      // 
      this.DataGridViewImageColumn1.HeaderText = "AddButtonColumn";
      this.DataGridViewImageColumn1.Name = "DataGridViewImageColumn1";
      this.DataGridViewImageColumn1.Width = 34;
      // 
      // DataGridViewTextBoxColumn1
      // 
      this.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.DataGridViewTextBoxColumn1.HeaderText = "LineTextColumn";
      this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
      // 
      // FooterLabel
      // 
      this.FooterLabel.AutoSize = true;
      this.FooterLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.FooterLabel.Location = new System.Drawing.Point(451, 10);
      this.FooterLabel.Name = "FooterLabel";
      this.FooterLabel.Size = new System.Drawing.Size(40, 13);
      this.FooterLabel.TabIndex = 5;
      this.FooterLabel.Text = "Footer:";
      // 
      // dgvHeaders
      // 
      this.dgvHeaders.AllowUserToAddRows = false;
      this.dgvHeaders.AllowUserToDeleteRows = false;
      this.dgvHeaders.AllowUserToResizeColumns = false;
      this.dgvHeaders.AllowUserToResizeRows = false;
      dataGridViewCellStyle4.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvHeaders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
      this.dgvHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
      dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
      dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvHeaders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
      this.dgvHeaders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvHeaders.ColumnHeadersVisible = false;
      this.dgvHeaders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AddButtonColumn,
            this.LineTextColumn});
      this.TableLayoutPanel1.SetColumnSpan(this.dgvHeaders, 2);
      dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
      dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
      dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.ControlText;
      dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
      this.dgvHeaders.DefaultCellStyle = dataGridViewCellStyle6;
      this.dgvHeaders.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
      this.dgvHeaders.Location = new System.Drawing.Point(3, 48);
      this.dgvHeaders.MultiSelect = false;
      this.dgvHeaders.Name = "dgvHeaders";
      dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
      dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
      dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.dgvHeaders.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
      this.dgvHeaders.RowHeadersVisible = false;
      dataGridViewCellStyle8.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvHeaders.RowsDefaultCellStyle = dataGridViewCellStyle8;
      this.dgvHeaders.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Courier New", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.dgvHeaders.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.dgvHeaders.ShowCellErrors = false;
      this.dgvHeaders.ShowEditingIcon = false;
      this.dgvHeaders.ShowRowErrors = false;
      this.dgvHeaders.Size = new System.Drawing.Size(397, 197);
      this.dgvHeaders.TabIndex = 7;
      // 
      // AddButtonColumn
      // 
      this.AddButtonColumn.HeaderText = "AddButtonColumn";
      this.AddButtonColumn.Name = "AddButtonColumn";
      this.AddButtonColumn.Width = 34;
      // 
      // LineTextColumn
      // 
      this.LineTextColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.LineTextColumn.HeaderText = "LineTextColumn";
      this.LineTextColumn.Name = "LineTextColumn";
      // 
      // btnAddHeader
      // 
      this.btnAddHeader.Image = Properties.Resources.add;
      this.btnAddHeader.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.btnAddHeader.Location = new System.Drawing.Point(3, 3);
      this.btnAddHeader.Name = "btnAddHeader";
      this.TableLayoutPanel1.SetRowSpan(this.btnAddHeader, 2);
      this.btnAddHeader.Size = new System.Drawing.Size(39, 39);
      this.btnAddHeader.TabIndex = 4;
      this.btnAddHeader.UseVisualStyleBackColor = true;
      // 
      // btnAddFooter
      // 
      this.btnAddFooter.Image = Properties.Resources.add;
      this.btnAddFooter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.btnAddFooter.Location = new System.Drawing.Point(406, 3);
      this.btnAddFooter.Name = "btnAddFooter";
      this.TableLayoutPanel1.SetRowSpan(this.btnAddFooter, 2);
      this.btnAddFooter.Size = new System.Drawing.Size(39, 39);
      this.btnAddFooter.TabIndex = 6;
      this.btnAddFooter.UseVisualStyleBackColor = true;
      // 
      // CFpmReceiptConfig
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.TableLayoutPanel1);
      this.Name = "CFpmReceiptConfig";
      this.Size = new System.Drawing.Size(807, 248);
      this.TableLayoutPanel1.ResumeLayout(false);
      this.TableLayoutPanel1.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgvFooters)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dgvHeaders)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion


    internal System.Windows.Forms.TableLayoutPanel TableLayoutPanel1;
    internal System.Windows.Forms.Label HeaderLabel;
    internal System.Windows.Forms.DataGridView dgvFooters;
    internal System.Windows.Forms.DataGridViewImageColumn DataGridViewImageColumn1;
    internal System.Windows.Forms.DataGridViewTextBoxColumn DataGridViewTextBoxColumn1;
    internal System.Windows.Forms.Label FooterLabel;
    internal System.Windows.Forms.DataGridView dgvHeaders;
    internal System.Windows.Forms.DataGridViewImageColumn AddButtonColumn;
    internal System.Windows.Forms.DataGridViewTextBoxColumn LineTextColumn;
    internal System.Windows.Forms.Button btnAddHeader;
    internal System.Windows.Forms.Button btnAddFooter;
  }
}
