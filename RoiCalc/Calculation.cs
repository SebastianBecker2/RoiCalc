using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoiCalc
{
    class Calculation
    {
        public Item Item { get; set; }

        public int Count { get; set; }

        public int Interval { get; set; }

        public ResultCollection Results { get; set; }
    }
}
