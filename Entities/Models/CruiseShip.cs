using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class CruiseShip
    {
        public CruiseShip()
        {
            OtherReports = new HashSet<OtherReport>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CruiseCompanyId { get; set; }

        public virtual CruiseCompany CruiseCompany { get; set; }
        public virtual ICollection<OtherReport> OtherReports { get; set; }
    }
}
