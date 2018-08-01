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
        public ICollection<Item> Items { get; set; }
        public Item SelectedItem { get; set; }

        public ItemSelectionDialog()
        {
            InitializeComponent();

            dgvItems.DoubleClick += btnOk_Click;
        }

        protected override void OnLoad(EventArgs e)
        {
            UpdateItemList(Items);

            base.OnLoad(e);
        }

        private void UpdateItemList(ICollection<Item> items)
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
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count == 0)
            {
                return;
            }

            SelectedItem = dgvItems.SelectedRows[0].Cells[0].Tag as Item;
            DialogResult = DialogResult.OK;
        }
    }
}
