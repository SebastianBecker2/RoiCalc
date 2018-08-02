using System;
using System.Collections.Generic;
using System.Text;

namespace CheckComboBoxTest {
    public class CCBoxItem {
        public int Value { get; set; }
        public string Name { get; set; }

        public CCBoxItem() {
        }

        public CCBoxItem(string name, int val) {
            this.Name = name;
            this.Value = val;
        }

        public override string ToString() {
            return string.Format("name: '{0}', value: {1}", Name, Value);
        }
    }
}
