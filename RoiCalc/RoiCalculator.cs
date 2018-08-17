using ImbaControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace RoiCalc
{
    public partial class RoiCalculator : Form
    {
        private const string ItemsFileName = @"items.roic";


        private IDictionary<string, Item> Items { get; set; }

        private ResultCollection Results { get; set; }

        private ISet<Calculation> Calculations { get; set; }
            = new HashSet<Calculation>();

        private Item SelectedItem
        {
            get => selected_item;
            set
            {
                selected_item = value;
                if (selected_item == null)
                {
                    lblItemName.Text = "Select an item";
                    return;
                }
                else
                {
                    lblItemName.Text = selected_item.Name;
                }
                pibItemImage.Image = selected_item?.Image;
            }
        }
        private Item selected_item;


        public RoiCalculator()
        {
            InitializeComponent();
            Items = ReadItems(Path.Combine(Directory.GetCurrentDirectory(), "Resources", ItemsFileName));
            dgvCalculations.CellContentClick += OnDgvCalculationsCellContentClick;
            SelectedItem = Items.FirstOrDefault().Value;
        }

        private void OnDgvCalculationsCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (!(senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn))
            {
                return;
            }

            // Ignore header
            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == 3)
            {
                Calculations.Remove(senderGrid.Rows[e.RowIndex].Tag as Calculation);
                UpdateCalculationsView(Calculations);
            }
            else if (e.ColumnIndex == 4)
            {
                Results += (senderGrid.Rows[e.RowIndex].Tag as Calculation).CalculateResults();
                UpdateResultView(Results);
            }
        }

        private IDictionary<string, Item> ReadItems(string path)
        {
            var res_path = Path.GetDirectoryName(path);

            var items = new Dictionary<string, Item>();

            var lines = File.ReadAllLines(path)
                .Select(l => l.Split(',', ';').Select(v => v.Trim()).ToList());

            foreach (var line in lines)
            {
                if (line.Count() < 4)
                {
                    continue;
                }

                if (!int.TryParse(line[0], out var type))
                {
                    continue;
                }

                if (!int.TryParse(line[2], out var count))
                {
                    continue;
                }

                if (!int.TryParse(line[3], out var interval))
                {
                    continue;
                }

                var item = new Item()
                {
                    Name = line[1],
                    Count = count,
                    Interval = interval,
                    Type = (ItemType)type,
                    Image = Properties.Resources.no_image,
                };

                try
                {
                    var file_path = Path.Combine(res_path, item.Name + ".png");
                    item.Image = Image.FromFile(file_path);
                }
                catch (FileNotFoundException) { }

                items.Add(line[1], item);
            }

            foreach (var line in lines)
            {
                if (line.Count() < 6)
                {
                    continue;
                }

                if (!int.TryParse(line[5], out var count))
                {
                    continue;
                }

                items[line[1]].AddIngredient(items[line[4]], count);

                if (line.Count() < 8)
                {
                    continue;
                }

                if (!int.TryParse(line[7], out count))
                {
                    continue;
                }

                items[line[1]].AddIngredient(items[line[6]], count);

                if (line.Count() < 10)
                {
                    continue;
                }

                if (!int.TryParse(line[9], out count))
                {
                    continue;
                }

                items[line[1]].AddIngredient(items[line[8]], count);
            }

            return items;
        }

        private void UpdateResultView(ResultCollection results)
        {
            dgvResults.Rows.Clear();

            if (results == null)
            {
                return;
            }

            foreach (var res in results)
            {
                var row = new DataGridViewRow();

                var name_cell = new DataGridViewTextImageCell()
                {
                    Value = res.Key.Name,
                    Image = res.Key.Image?.Resize(15, 15),
                };
                row.Cells.Add(name_cell);

                var count_cell = new DataGridViewTextBoxCell()
                {
                    Value = res.Value
                };
                row.Cells.Add(count_cell);

                dgvResults.Rows.Add(row);
            }
        }

        private void UpdateCalculationsView(ISet<Calculation> calculations)
        {
            dgvCalculations.Rows.Clear();

            if (calculations == null)
            {
                return;
            }

            foreach (var calculation in calculations)
            {
                var row = new DataGridViewRow();

                var name_cell = new DataGridViewTextImageCell()
                {
                    Value = calculation.Item.Name,
                    Image = calculation.Item.Image?.Resize(15, 15),
                };
                row.Cells.Add(name_cell);

                var count_cell = new DataGridViewTextBoxCell()
                {
                    Value = calculation.Count
                };
                row.Cells.Add(count_cell);

                var interval_cell = new DataGridViewTextBoxCell()
                {
                    Value = calculation.Interval
                };
                row.Cells.Add(interval_cell);

                row.Tag = calculation;

                dgvCalculations.Rows.Add(row);
            }
        }

        private void SelectCalculation(Calculation calculation)
        {
            dgvCalculations.ClearSelection();

            var selected_row = dgvCalculations.Rows
                .OfType<DataGridViewRow>()
                .FirstOrDefault(r => calculation.Equals(r.Tag as Calculation));
            if (selected_row != null)
            {
                dgvCalculations.CurrentCell = selected_row.Cells[0];
                selected_row.Selected = true;
            }
        }

        private void BtnCalc_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                return;
            }

            if (!int.TryParse(txtCount.Text, out var count))
            {
                return;
            }

            if (!int.TryParse(txtInterval.Text, out var interval))
            {
                return;
            }

            var calculation = new Calculation(SelectedItem, count, interval);
            Results = calculation.CalculateResults();
            UpdateResultView(Results);
            if (Calculations.Add(calculation))
            {
                UpdateCalculationsView(Calculations);
            }
            SelectCalculation(calculation);
        }

        private void BtnClearResult_Click(object sender, EventArgs e)
        {
            Results = new ResultCollection();
            UpdateResultView(Results);
        }

        private void OnSelectItem(object sender, EventArgs e)
        {
            using (var dlg = new ItemSelectionDialog())
            {
                dlg.Items = Items.Values;
                dlg.SelectedItem = SelectedItem;

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                SelectedItem = dlg.SelectedItem;
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new AboutDialog())
            {
                dlg.ShowDialog();
            }
        }

        private void LoadItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Multiselect = false;
                dlg.InitialDirectory = Directory.GetCurrentDirectory();
                dlg.Filter = @"RoiCalc Items File (*.roic)|*.roic|All files (*.*)|*.*";

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                Items = ReadItems(dlg.FileName);
                Results = null;
                UpdateResultView(Results);
                Calculations = new HashSet<Calculation>();
                UpdateCalculationsView(Calculations);
            }
        }
    }
}
