using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class MonthlyRevDto
    {
        public int OutletId { get; set; }
        public List<int[][]> Revenues { get; set; } //[month][revenue]
    }
}
