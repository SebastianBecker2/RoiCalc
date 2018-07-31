namespace RoiCalc
{
    partial class RoiCalc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbItems = new ImageComboBox.CImageComboBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.clmResultName = new ImbaControls.DataGridViewTextImageColumn();
            this.clmResultCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dgvCalculations = new System.Windows.Forms.DataGridView();
            this.clmCalculationName = new ImbaControls.DataGridViewTextImageColumn();
            this.clmCalculationCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCalculationInterval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmCalculationAddResult = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnClearResult = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculations)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbItems
            // 
            this.cmbItems.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbItems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbItems.FormattingEnabled = true;
            this.cmbItems.ImageList = null;
            this.cmbItems.Indent = 0;
            this.cmbItems.ItemHeight = 15;
            this.cmbItems.Location = new System.Drawing.Point(64, 12);
            this.cmbItems.Name = "cmbItems";
            this.cmbItems.Size = new System.Drawing.Size(121, 21);
            this.cmbItems.TabIndex = 0;
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeColumns = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmResultName,
            this.clmResultCount});
            this.dgvResults.Location = new System.Drawing.Point(334, 15);
            this.dgvResults.MultiSelect = false;
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.RowHeadersVisible = false;
            this.dgvResults.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.ShowEditingIcon = false;
            this.dgvResults.ShowRowErrors = false;
            this.dgvResults.Size = new System.Drawing.Size(249, 229);
            this.dgvResults.TabIndex = 1;
            this.dgvResults.TabStop = false;
            // 
            // clmResultName
            // 
            this.clmResultName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmResultName.HeaderText = "Name";
            this.clmResultName.Image = null;
            this.clmResultName.Name = "clmResultName";
            this.clmResultName.ReadOnly = true;
            this.clmResultName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmResultCount
            // 
            this.clmResultCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmResultCount.FillWeight = 50F;
            this.clmResultCount.HeaderText = "Count";
            this.clmResultCount.Name = "clmResultCount";
            this.clmResultCount.ReadOnly = true;
            this.clmResultCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(64, 39);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(100, 20);
            this.txtCount.TabIndex = 1;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(64, 65);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(100, 20);
            this.txtInterval.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Interval:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Item:";
            // 
            // dgvCalculations
            // 
            this.dgvCalculations.AllowUserToAddRows = false;
            this.dgvCalculations.AllowUserToDeleteRows = false;
            this.dgvCalculations.AllowUserToResizeColumns = false;
            this.dgvCalculations.AllowUserToResizeRows = false;
            this.dgvCalculations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCalculationName,
            this.clmCalculationCount,
            this.clmCalculationInterval,
            this.clmCalculationAddResult});
            this.dgvCalculations.Location = new System.Drawing.Point(12, 91);
            this.dgvCalculations.MultiSelect = false;
            this.dgvCalculations.Name = "dgvCalculations";
            this.dgvCalculations.ReadOnly = true;
            this.dgvCalculations.RowHeadersVisible = false;
            this.dgvCalculations.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvCalculations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCalculations.ShowCellErrors = false;
            this.dgvCalculations.ShowCellToolTips = false;
            this.dgvCalculations.ShowEditingIcon = false;
            this.dgvCalculations.ShowRowErrors = false;
            this.dgvCalculations.Size = new System.Drawing.Size(316, 153);
            this.dgvCalculations.TabIndex = 7;
            this.dgvCalculations.TabStop = false;
            // 
            // clmCalculationName
            // 
            this.clmCalculationName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmCalculationName.HeaderText = "Name";
            this.clmCalculationName.Image = null;
            this.clmCalculationName.Name = "clmCalculationName";
            this.clmCalculationName.ReadOnly = true;
            this.clmCalculationName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmCalculationCount
            // 
            this.clmCalculationCount.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmCalculationCount.FillWeight = 50F;
            this.clmCalculationCount.HeaderText = "Count";
            this.clmCalculationCount.Name = "clmCalculationCount";
            this.clmCalculationCount.ReadOnly = true;
            this.clmCalculationCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmCalculationInterval
            // 
            this.clmCalculationInterval.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmCalculationInterval.FillWeight = 50F;
            this.clmCalculationInterval.HeaderText = "Interval";
            this.clmCalculationInterval.Name = "clmCalculationInterval";
            this.clmCalculationInterval.ReadOnly = true;
            this.clmCalculationInterval.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // clmCalculationAddResult
            // 
            this.clmCalculationAddResult.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clmCalculationAddResult.HeaderText = "Add Result";
            this.clmCalculationAddResult.Name = "clmCalculationAddResult";
            this.clmCalculationAddResult.ReadOnly = true;
            this.clmCalculationAddResult.Text = "->";
            this.clmCalculationAddResult.UseColumnTextForButtonValue = true;
            this.clmCalculationAddResult.Width = 65;
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(196, 62);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(54, 23);
            this.btnCalc.TabIndex = 3;
            this.btnCalc.Text = "Calc";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnClearResult
            // 
            this.btnClearResult.Location = new System.Drawing.Point(256, 62);
            this.btnClearResult.Name = "btnClearResult";
            this.btnClearResult.Size = new System.Drawing.Size(72, 23);
            this.btnClearResult.TabIndex = 8;
            this.btnClearResult.Text = "Clear";
            this.btnClearResult.UseVisualStyleBackColor = true;
            this.btnClearResult.Click += new System.EventHandler(this.btnClearResult_Click);
            // 
            // RoiCalc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 254);
            this.Controls.Add(this.btnClearResult);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.dgvCalculations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.cmbItems);
            this.Name = "RoiCalc";
            this.Text = "RoiCalc";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculations)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ImageComboBox.CImageComboBox cmbItems;
        private System.Windows.Forms.DataGridView dgvResults;
        private ImbaControls.DataGridViewTextImageColumn clmResultName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmResultCount;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvCalculations;
        private System.Windows.Forms.Button btnCalc;
        private ImbaControls.DataGridViewTextImageColumn clmCalculationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCalculationCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCalculationInterval;
        private System.Windows.Forms.DataGridViewButtonColumn clmCalculationAddResult;
        private System.Windows.Forms.Button btnClearResult;
    }
}

