namespace RoiCalc
{
    partial class RoiCalculator
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
            this.clmCaclulationDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.clmCalculationAddResult = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnCalc = new System.Windows.Forms.Button();
            this.btnClearResult = new System.Windows.Forms.Button();
            this.btnLoadItemsFromFile = new System.Windows.Forms.Button();
            this.dataGridViewTextImageColumn1 = new ImbaControls.DataGridViewTextImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextImageColumn2 = new ImbaControls.DataGridViewTextImageColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmbSelectItem = new System.Windows.Forms.Button();
            this.pibItemImage = new System.Windows.Forms.PictureBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.btnAbout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibItemImage)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvResults
            // 
            this.dgvResults.AllowUserToAddRows = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AllowUserToResizeColumns = false;
            this.dgvResults.AllowUserToResizeRows = false;
            this.dgvResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dgvResults.Size = new System.Drawing.Size(182, 258);
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
            this.clmResultCount.FillWeight = 60F;
            this.clmResultCount.HeaderText = "Count";
            this.clmResultCount.Name = "clmResultCount";
            this.clmResultCount.ReadOnly = true;
            this.clmResultCount.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtCount
            // 
            this.txtCount.Location = new System.Drawing.Point(64, 79);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(100, 20);
            this.txtCount.TabIndex = 1;
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(64, 105);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(100, 20);
            this.txtInterval.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Interval:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 23);
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
            this.dgvCalculations.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvCalculations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCalculations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmCalculationName,
            this.clmCalculationCount,
            this.clmCalculationInterval,
            this.clmCaclulationDelete,
            this.clmCalculationAddResult});
            this.dgvCalculations.Location = new System.Drawing.Point(12, 133);
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
            this.dgvCalculations.Size = new System.Drawing.Size(316, 140);
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
            // clmCaclulationDelete
            // 
            this.clmCaclulationDelete.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.clmCaclulationDelete.HeaderText = "Delete";
            this.clmCaclulationDelete.Name = "clmCaclulationDelete";
            this.clmCaclulationDelete.ReadOnly = true;
            this.clmCaclulationDelete.Text = "X";
            this.clmCaclulationDelete.UseColumnTextForButtonValue = true;
            this.clmCaclulationDelete.Width = 44;
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
            this.btnCalc.Location = new System.Drawing.Point(211, 102);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(54, 23);
            this.btnCalc.TabIndex = 3;
            this.btnCalc.Text = "Calc";
            this.btnCalc.UseVisualStyleBackColor = true;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // btnClearResult
            // 
            this.btnClearResult.Location = new System.Drawing.Point(271, 102);
            this.btnClearResult.Name = "btnClearResult";
            this.btnClearResult.Size = new System.Drawing.Size(57, 23);
            this.btnClearResult.TabIndex = 8;
            this.btnClearResult.Text = "Clear";
            this.btnClearResult.UseVisualStyleBackColor = true;
            this.btnClearResult.Click += new System.EventHandler(this.btnClearResult_Click);
            // 
            // btnLoadItemsFromFile
            // 
            this.btnLoadItemsFromFile.Location = new System.Drawing.Point(244, 73);
            this.btnLoadItemsFromFile.Name = "btnLoadItemsFromFile";
            this.btnLoadItemsFromFile.Size = new System.Drawing.Size(84, 23);
            this.btnLoadItemsFromFile.TabIndex = 9;
            this.btnLoadItemsFromFile.Text = "Load Items...";
            this.btnLoadItemsFromFile.UseVisualStyleBackColor = true;
            this.btnLoadItemsFromFile.Click += new System.EventHandler(this.btnLoadItemsFromFile_Click);
            // 
            // dataGridViewTextImageColumn1
            // 
            this.dataGridViewTextImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextImageColumn1.HeaderText = "Name";
            this.dataGridViewTextImageColumn1.Image = null;
            this.dataGridViewTextImageColumn1.Name = "dataGridViewTextImageColumn1";
            this.dataGridViewTextImageColumn1.ReadOnly = true;
            this.dataGridViewTextImageColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.FillWeight = 50F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Count";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextImageColumn2
            // 
            this.dataGridViewTextImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextImageColumn2.HeaderText = "Name";
            this.dataGridViewTextImageColumn2.Image = null;
            this.dataGridViewTextImageColumn2.Name = "dataGridViewTextImageColumn2";
            this.dataGridViewTextImageColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.FillWeight = 50F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Count";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.FillWeight = 50F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Interval";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // cmbSelectItem
            // 
            this.cmbSelectItem.Location = new System.Drawing.Point(244, 44);
            this.cmbSelectItem.Name = "cmbSelectItem";
            this.cmbSelectItem.Size = new System.Drawing.Size(84, 23);
            this.cmbSelectItem.TabIndex = 10;
            this.cmbSelectItem.Text = "Select Item...";
            this.cmbSelectItem.UseVisualStyleBackColor = true;
            this.cmbSelectItem.Click += new System.EventHandler(this.cmbSelectItem_Click);
            // 
            // pibItemImage
            // 
            this.pibItemImage.Location = new System.Drawing.Point(64, 23);
            this.pibItemImage.Name = "pibItemImage";
            this.pibItemImage.Size = new System.Drawing.Size(50, 50);
            this.pibItemImage.TabIndex = 11;
            this.pibItemImage.TabStop = false;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(120, 23);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(86, 13);
            this.lblItemName.TabIndex = 12;
            this.lblItemName.Text = "[Item name here]";
            // 
            // btnAbout
            // 
            this.btnAbout.Location = new System.Drawing.Point(244, 15);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(84, 23);
            this.btnAbout.TabIndex = 13;
            this.btnAbout.Text = "About...";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // RoiCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 283);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.lblItemName);
            this.Controls.Add(this.pibItemImage);
            this.Controls.Add(this.cmbSelectItem);
            this.Controls.Add(this.btnLoadItemsFromFile);
            this.Controls.Add(this.btnClearResult);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.dgvCalculations);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.dgvResults);
            this.Name = "RoiCalculator";
            this.Text = "RoiCalc";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCalculations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pibItemImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvCalculations;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Button btnClearResult;
        private System.Windows.Forms.Button btnLoadItemsFromFile;
        private ImbaControls.DataGridViewTextImageColumn clmCalculationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCalculationCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmCalculationInterval;
        private System.Windows.Forms.DataGridViewButtonColumn clmCaclulationDelete;
        private System.Windows.Forms.DataGridViewButtonColumn clmCalculationAddResult;
        private ImbaControls.DataGridViewTextImageColumn dataGridViewTextImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private ImbaControls.DataGridViewTextImageColumn dataGridViewTextImageColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private ImbaControls.DataGridViewTextImageColumn clmResultName;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmResultCount;
        private System.Windows.Forms.Button cmbSelectItem;
        private System.Windows.Forms.PictureBox pibItemImage;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnAbout;
    }
}

