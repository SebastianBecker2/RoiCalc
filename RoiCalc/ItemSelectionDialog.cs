using CheckComboBoxTest;
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
            get => selected_item;
            set
            {
                selected_item = value;
                rcvCurrentRecipe.Visible = selected_item != null;
                rcvCurrentRecipe.Recipe = selected_item;
            }
        }
        private Item selected_item;

        private bool updating_item_list = false;
        private bool updating_item_list_selection = false;

        public ItemSelectionDialog()
        {
            InitializeComponent();

            foreach (ItemType type in Enum.GetValues(typeof(ItemType)))
            {
                cmbFilter.Items.Add(new CCBoxItem()
                {
                    Name = type.ToPrettyString(),
                    BackColor = type.ToBackColor()
                });
                cmbFilter.SetItemChecked((int)type - 1, true);
            }
            cmbFilter.ItemCheck += CmbFilter_ItemCheck;
            txtFilter.TextChanged += TxtFilter_TextChanged;

            dgvItems.DoubleClick += BtnOk_Click;
            dgvItems.SelectionChanged += DgvItems_SelectionChanged;

            rcvCurrentRecipe.IngredientClick += RcvCurrentRecipe_IngredientClick;

        }

        private void TxtFilter_TextChanged(object sender, EventArgs e) {
            var filtered_items = GetFilteredItems(txtFilter.Text, GetFilterTypes());
            if (filtered_items.Count() == 1)
            {
                SelectedItem = filtered_items.First();
            }
            UpdateItemList(filtered_items);
            UpdateItemListSelection(SelectedItem);
        }

        private void CmbFilter_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            var filtered_items = GetFilteredItems(txtFilter.Text, GetFilterTypes(e));
            if (filtered_items.Count() == 1)
            {
                SelectedItem = filtered_items.First();
            }
            UpdateItemList(filtered_items);
            UpdateItemListSelection(SelectedItem);
        }

        private void RcvCurrentRecipe_IngredientClick(
            object sender,
            RecipeView.IngredientClickEventArgs e)
        {
            var filtered_items = GetFilteredItems(txtFilter.Text, GetFilterTypes());
            UpdateItemList(filtered_items);
            SelectedItem = e.ClickedIngredient;
            UpdateItemListSelection(SelectedItem);
        }

        private IEnumerable<int> GetFilterTypes(ItemCheckEventArgs e = null)
        {
            for (var i = 0; i < cmbFilter.Items.Count; i++)
            {
                if (e?.Index == i)
                {
                    if (e.NewValue == CheckState.Checked)
                    {
                        yield return i;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (!cmbFilter.GetItemChecked(i))
                {
                    continue;
                }
                yield return i;
            }
        }

        private void DgvItems_SelectionChanged(object sender, EventArgs e)
        {
            if (updating_item_list || updating_item_list_selection)
            {
                return;
            }

            if (dgvItems.SelectedRows.Count >= 1)
            {
                SelectedItem = dgvItems.SelectedRows[0].Tag as Item;
                return;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            UpdateItemList(Items);
            UpdateItemListSelection(SelectedItem);

            base.OnLoad(e);
        }

        private void UpdateItemListSelection(Item selected_item)
        {
            updating_item_list_selection = true;
            try
            {
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
            finally
            {
                updating_item_list_selection = false;
            }
        }

        private void UpdateItemList(IEnumerable<Item> items)
        {
            updating_item_list = true;
            try
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
                    };
                    row.Cells.Add(name_cell);
                    row.DefaultCellStyle.BackColor = item.Type.ToBackColor();
                    row.Tag = item;

                    dgvItems.Rows.Add(row);
                }
            }
            finally
            {
                updating_item_list = false;
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                DialogResult = DialogResult.OK;
            }
        }
        
        private IEnumerable<Item> GetFilteredItems(
            string filter_text,
            IEnumerable<int> filter_types)
        {
            var items = Items;

            if (!string.IsNullOrWhiteSpace(filter_text))
            {
                filter_text = filter_text.ToLower();
                items = items.Where(i => i.Name.ToLower().Contains(filter_text));
            }

            if (filter_types != null)
            {
                items = items.Where(i => filter_types.Contains((int)i.Type - 1));
            }

            return items;
        }
    }
}
