
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class RevenueOverViewDto
    {
        public Dictionary<int, int?> YTDs { get; set; }
        public Dictionary<int, int?> MTDs { get; set; }
        public Dictionary<int, int?> YesterdaysRevs { get; set; }
        public int[][] RevsAllOutlets1Month { get; set; }
        public int[][] RevsAllOutletsAllMonths { get; set; }
    }
}
