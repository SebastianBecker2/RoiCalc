using ImageComboBox;
using ImbaControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoiCalc
{
    public partial class RoiCalc : Form
    {
        private const string ItemsFileName = @"items.roic";


        private IDictionary<string, Item> Items { get; set; }

        private ResultCollection Results { get; set; }

        private IList<Calculation> Calculations { get; set; }
            = new List<Calculation>();

        private Item SelectedItem
        {
            get { return selected_item; }
            set
            {
                selected_item = value;
                if (selected_item == null)
                {
                    lblItemName.Text = "Select an item";
                    return;
                } else
                {
                    lblItemName.Text = selected_item.Name;
                }
                pibItemImage.Image = selected_item?.Image;
            }
        }
        private Item selected_item;


        public RoiCalc()
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
                Calculations.RemoveAt(e.RowIndex);
                UpdateCalculationsView(Calculations);
            }
            else if (e.ColumnIndex == 4)
            {
                Results += Calculations[e.RowIndex].Results;
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

                if (!int.TryParse(line[0], out int type))
                {
                    continue;
                }

                if (!int.TryParse(line[2], out int count))
                {
                    continue;
                }

                if (!int.TryParse(line[3], out int interval))
                {
                    continue;
                }

                var item = new Item()
                {
                    Name = line[1],
                    Count = count,
                    Interval = interval,
                    Type = (ItemType)type,
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

                if (!int.TryParse(line[5], out int count))
                {
                    continue;
                }

                items[line[1]].AddRequirement(items[line[4]], count);

                if (line.Count() < 8)
                {
                    continue;
                }

                if (!int.TryParse(line[7], out count))
                {
                    continue;
                }

                items[line[1]].AddRequirement(items[line[6]], count);

                if (line.Count() < 10)
                {
                    continue;
                }

                if (!int.TryParse(line[9], out count))
                {
                    continue;
                }

                items[line[1]].AddRequirement(items[line[8]], count);
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

        private void UpdateCalculationsView(IList<Calculation> calculations)
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

                dgvCalculations.Rows.Add(row);
            }
        }

        private ResultCollection CalculateResults(Item item, int count, int interval)
        {
            var request = (float)count / interval;
            return CalculateResults(item, request);
        }

        private ResultCollection CalculateResults(Item item, float request)
        {
            var results = new ResultCollection();
            var production = (float)item.Count / item.Interval;
            var required_count = request / production;
            results.Add(item, required_count);

            foreach (var requirement in item.Requirements)
            {
                var requirement_request = ((required_count * requirement.Value) / item.Interval);
                var res = CalculateResults(requirement.Key, requirement_request);
                foreach (var r in res)
                {
                    if (results.ContainsKey(r.Key))
                    {
                        results[r.Key] += r.Value;
                    }
                    else
                    {
                        results.Add(r.Key, r.Value);
                    }
                }
            }

            return results;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                return;
            }

            if (!int.TryParse(txtCount.Text, out int count))
            {
                return;
            }

            if (!int.TryParse(txtInterval.Text, out int interval))
            {
                return;
            }

            Results = CalculateResults(SelectedItem, count, interval);
            UpdateResultView(Results);
            Calculations.Add(new Calculation()
            {
                Item = SelectedItem,
                Count = count,
                Interval = interval,
                Results = Results
            });
            UpdateCalculationsView(Calculations);
        }

        private void btnClearResult_Click(object sender, EventArgs e)
        {
            Results = new ResultCollection();
            UpdateResultView(Results);
        }

        private void btnLoadItemsFromFile_Click(object sender, EventArgs e)
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
                Calculations = new List<Calculation>();
                UpdateCalculationsView(Calculations);
            }
        }

        private void cmbSelectItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new ItemSelectionDialog())
            {
                dlg.Items = Items.Values;

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                SelectedItem = dlg.SelectedItem;
            }
        }
    }
}
