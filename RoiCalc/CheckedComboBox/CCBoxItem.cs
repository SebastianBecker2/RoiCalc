using System.Drawing;

namespace CheckComboBoxTest
{
    public class CCBoxItem {
        public string Name { get; set; }
        public Color BackColor { get; set; }

        public CCBoxItem() {
        }

        public CCBoxItem(string name) {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }
}
