using ImbaControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace RoiCalc
{
    partial class ItemSelectionDialog : Form
    {
        public IEnumerable<Item> Items { get; set; }

        public Item SelectedItem
        {
            get { return selected_item; }
            set
            {
                selected_item = value;
                recipeView1.Visible = selected_item != null;
                recipeView1.SetItem(selected_item);
            }
        }
        private Item selected_item;

        public ItemSelectionDialog()
        {
            InitializeComponent();

            dgvItems.DoubleClick += btnOk_Click;
            dgvItems.SelectionChanged += DgvItems_SelectionChanged;
        }

        private void DgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count >= 1)
            {
                SelectedItem = dgvItems.SelectedRows[0].Tag as Item;
                return;
            }

            if (dgvItems.Rows.Count == 1)
            {
                SelectedItem = dgvItems.Rows[0].Tag as Item;
                return;
            }

            SelectedItem = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            UpdateItemList(Items, SelectedItem);

            base.OnLoad(e);
        }

        private void UpdateItemList(IEnumerable<Item> items, Item selected_item)
        {
            dgvItems.Rows.Clear();

            if (items == null)
            {
                return;
            }

            selected_item = items.FirstOrDefault(i => i == selected_item);

            foreach (var item in items)
            {
                var row = new DataGridViewRow();

                var name_cell = new DataGridViewTextImageCell()
                {
                    Value = item.Name,
                    Image = item.Image?.Resize(15, 15),
                };
                row.Cells.Add(name_cell);
                row.DefaultCellStyle.BackColor = item.Type.ToBackColor();
                row.Tag = item;

                dgvItems.Rows.Add(row);
            }

            dgvItems.ClearSelection();

            if (selected_item == null)
            {
                return;
            }

            var selected_row = dgvItems.Rows
                .OfType<DataGridViewRow>()
                .FirstOrDefault(r => r.Tag == selected_item);
            if (selected_row != null)
            {
                dgvItems.CurrentCell = selected_row.Cells[0];
                selected_row.Selected = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            var items = Items.Where(i =>
                string.IsNullOrWhiteSpace(txtFilter.Text) ||
                i.Name.ToLower().StartsWith(txtFilter.Text.ToLower()));

            UpdateItemList(items, SelectedItem);
        }
    }
}
