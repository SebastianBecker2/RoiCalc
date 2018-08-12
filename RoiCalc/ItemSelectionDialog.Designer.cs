namespace RoiCalc
{
    partial class ItemSelectionDialog
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
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.clmItemName = new ImbaControls.DataGridViewTextImageColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dataGridViewTextImageColumn1 = new ImbaControls.DataGridViewTextImageColumn();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rcvCurrentRecipe = new RoiCalc.RecipeView();
            this.cmbFilter = new CheckComboBoxTest.CheckedComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AllowUserToResizeColumns = false;
            this.dgvItems.AllowUserToResizeRows = false;
            this.dgvItems.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.ColumnHeadersVisible = false;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmItemName});
            this.dgvItems.Location = new System.Drawing.Point(12, 38);
            this.dgvItems.MultiSelect = false;
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.RowHeadersVisible = false;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.ShowCellErrors = false;
            this.dgvItems.ShowCellToolTips = false;
            this.dgvItems.ShowEditingIcon = false;
            this.dgvItems.Size = new System.Drawing.Size(201, 325);
            this.dgvItems.StandardTab = true;
            this.dgvItems.TabIndex = 2;
            // 
            // clmItemName
            // 
            this.clmItemName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmItemName.HeaderText = "Name";
            this.clmItemName.Image = null;
            this.clmItemName.Name = "clmItemName";
            this.clmItemName.ReadOnly = true;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(377, 339);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(458, 339);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // dataGridViewTextImageColumn1
            // 
            this.dataGridViewTextImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextImageColumn1.HeaderText = "Name";
            this.dataGridViewTextImageColumn1.Image = null;
            this.dataGridViewTextImageColumn1.Name = "dataGridViewTextImageColumn1";
            this.dataGridViewTextImageColumn1.ReadOnly = true;
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(50, 12);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(147, 20);
            this.txtFilter.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Filter:";
            // 
            // rcvCurrentRecipe
            // 
            this.rcvCurrentRecipe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rcvCurrentRecipe.Location = new System.Drawing.Point(219, 12);
            this.rcvCurrentRecipe.MinimumSize = new System.Drawing.Size(316, 255);
            this.rcvCurrentRecipe.Name = "rcvCurrentRecipe";
            this.rcvCurrentRecipe.Recipe = null;
            this.rcvCurrentRecipe.Size = new System.Drawing.Size(316, 323);
            this.rcvCurrentRecipe.TabIndex = 5;
            // 
            // cmbFilter
            // 
            this.cmbFilter.CheckOnClick = true;
            this.cmbFilter.DisplayMember = "Name";
            this.cmbFilter.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cmbFilter.DropDownHeight = 1;
            this.cmbFilter.DropDownWidth = 100;
            this.cmbFilter.FormattingEnabled = true;
            this.cmbFilter.IntegralHeight = false;
            this.cmbFilter.Location = new System.Drawing.Point(196, 12);
            this.cmbFilter.MaxDropDownItems = 10;
            this.cmbFilter.Name = "cmbFilter";
            this.cmbFilter.Size = new System.Drawing.Size(17, 21);
            this.cmbFilter.TabIndex = 6;
            this.cmbFilter.ValueSeparator = ", ";
            // 
            // ItemSelectionDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(545, 374);
            this.Controls.Add(this.cmbFilter);
            this.Controls.Add(this.rcvCurrentRecipe);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dgvItems);
            this.MinimumSize = new System.Drawing.Size(561, 345);
            this.Name = "ItemSelectionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "RoiCalc - Select Item";
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvItems;
        private ImbaControls.DataGridViewTextImageColumn clmItemName;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private ImbaControls.DataGridViewTextImageColumn dataGridViewTextImageColumn1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label2;
        private RecipeView rcvCurrentRecipe;
        private CheckComboBoxTest.CheckedComboBox cmbFilter;
    }
}