using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoiCalc
{
    class Item
    {
        public int Count { get; set; }
        public int Interval { get; set; }
        public string Name { get; set; }
        public Image Image { get; set; }

        public IDictionary<Item, int> Requirements { get; set; } = new Dictionary<Item, int>();

        public Item() { }

        public void AddRequirement(Item item, int count)
        {
            Requirements.Add(item, count);
        }
    }
}
