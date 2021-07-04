using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class RevenueOverViewDto
    {
        public int Month { get; set; }
        public int OutletId { get; set; }
        public int Revenue { get; set; }
        public List<int> Revenues { get; set; }

    }
}
