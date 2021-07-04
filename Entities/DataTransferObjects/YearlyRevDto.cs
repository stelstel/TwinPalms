using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class YearlyRevDto
    {
        public List<MonthlyRevDto> MonthlyRevs { get; set; }
    }
}
