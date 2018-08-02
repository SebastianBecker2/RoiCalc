using System.Collections.Generic;
using System.Drawing;

namespace RoiCalc
{
    class Item
    {
        public ItemType Type { get; set; }
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
