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

        public IDictionary<Item, int> Ingredients { get; set; } = new Dictionary<Item, int>();

        public Item() { }

        public void AddIngredient(Item item, int count)
        {
            Ingredients.Add(item, count);
        }
    }
}
