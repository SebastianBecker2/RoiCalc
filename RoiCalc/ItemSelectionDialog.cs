using ImbaControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                SelectedItem = dgvItems.SelectedRows[0].Cells[0].Tag as Item;
                return;
            }

            if (dgvItems.Rows.Count == 1)
            {
                SelectedItem = dgvItems.Rows[0].Cells[0].Tag as Item;
                return;
            }

            SelectedItem = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            UpdateItemList(Items);

            base.OnLoad(e);
        }

        private void UpdateItemList(IEnumerable<Item> items)
        {
            dgvItems.Rows.Clear();

            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                var row = new DataGridViewRow();

                var name_cell = new DataGridViewTextImageCell()
                {
                    Value = item.Name,
                    Image = item.Image?.Resize(15, 15),
                    Tag = item,
                };
                row.Cells.Add(name_cell);
                row.DefaultCellStyle.BackColor = item.Type.ToBackColor();

                dgvItems.Rows.Add(row);
            }

            dgvItems.ClearSelection();
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

            UpdateItemList(items);
        }
    }
}
