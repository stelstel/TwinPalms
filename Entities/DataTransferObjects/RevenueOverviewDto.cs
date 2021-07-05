using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class RevenueOverviewDto
    {
        public Dictionary<int, int?> YTDs { get; set; }
        public Dictionary<int, int?> MTDs { get; set; }
        public Dictionary<int, int?> YesterdaysRevs { get; set; }

        //public int Month { get; set; }
        //public int OutletId { get; set; }
        //public int Revenue { get; set; }
        //public List<int> Revenues { get; set; }

        public YearlyRevDto YearlyRev { get; set; }

    }
}
