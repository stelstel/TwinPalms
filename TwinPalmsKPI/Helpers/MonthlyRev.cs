using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPalmsKPI.Helpers
{
    public class MonthlyRev
    {
        public int OutletId { get; set; }
        public List<int[][]> Revenues { get; set; } //[month][revenue]
    }
}
