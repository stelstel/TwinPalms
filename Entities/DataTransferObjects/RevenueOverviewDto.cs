
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class RevenueOverViewDto
    {
        public Dictionary<int, int?> MonthlyRevs { get; set; }
        public Dictionary<int, int?> YTDs { get; set; }
        public Dictionary<int, int?> MTDs { get; set; }
        public Dictionary<int, int?> YesterdaysRevs { get; set; }
    }
}
