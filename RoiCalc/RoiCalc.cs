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


        public RoiCalc()
        {
            InitializeComponent();
            Items = ReadItems(Path.Combine(Directory.GetCurrentDirectory(), "Resources", ItemsFileName));
            UpdateItemsComboBox(Items);
            dgvCalculations.CellContentClick += OnDgvCalculationsCellContentClick;
        }

        private void OnDgvCalculationsCellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                Results += Calculations[e.RowIndex].Results;
                UpdateResultView(Results);
            }
        }

        private void UpdateItemsComboBox(IDictionary<string, Item> items)
        {
            cmbItems.Items.Clear();
            var image_list = new ImageList();

            foreach (var item in items.Values)
            {
                if (item.Image != null)
                {
                    var image_index = image_list.Images.Count;
                    image_list.Images.Add(item.Image);
                    cmbItems.Items.Add(new ImageComboBoxItem(image_index, item.Name, 0));
                }
                else
                {
                    cmbItems.Items.Add(new ImageComboBoxItem(item.Name, 0));
                }
            }

            cmbItems.ImageList = image_list;
            cmbItems.SelectedIndex = 0;
        }

        private IDictionary<string, Item> ReadItems(string path)
        {
            var res_path = Path.GetDirectoryName(path);

            var items = new Dictionary<string, Item>();

            var lines = File.ReadAllLines(path)
                .Select(l => l.Split(',', ';').Select(v => v.Trim()).ToList());

            foreach (var line in lines)
            {
                if (line.Count() < 3)
                {
                    continue;
                }

                if (!int.TryParse(line[1], out int count))
                {
                    continue;
                }

                if (!int.TryParse(line[2], out int interval))
                {
                    continue;
                }

                var item = new Item()
                {
                    Name = line[0],
                    Count = count,
                    Interval = interval,
                };

                try
                {
                    var file_path = Path.Combine(res_path, item.Name + ".png");
                    item.Image = Image.FromFile(file_path);
                }
                catch (FileNotFoundException) { }

                items.Add(line[0], item);
            }

            foreach (var line in lines)
            {
                if (line.Count() < 5)
                {
                    continue;
                }

                if (!int.TryParse(line[4], out int count))
                {
                    continue;
                }

                items[line[0]].AddRequirement(items[line[3]], count);

                if (line.Count() < 7)
                {
                    continue;
                }

                if (!int.TryParse(line[6], out count))
                {
                    continue;
                }

                items[line[0]].AddRequirement(items[line[5]], count);

                if (line.Count() < 9)
                {
                    continue;
                }

                if (!int.TryParse(line[8], out count))
                {
                    continue;
                }

                items[line[0]].AddRequirement(items[line[7]], count);
            }

            return items;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            if (!Items.TryGetValue(cmbItems.Text, out Item item))
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

            Results = CalculateResults(item, count, interval);
            UpdateResultView(Results);
            Calculations.Add(new Calculation()
            {
                Item = item,
                Count = count,
                Interval = interval,
                Results = Results
            });
            UpdateCalculationsView(Calculations);
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

        private void btnClearResult_Click(object sender, EventArgs e)
        {
            Results = new ResultCollection();
            UpdateResultView(Results);
        }
    }
}
