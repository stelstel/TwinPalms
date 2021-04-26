using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Outlet
    {
        // TODO
        //public Outlet()
        //{
        //    FbReports = new HashSet<FbReport>();
        //    OutletUsers = new HashSet<OutletUser>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }

        public virtual ICollection<FbReport> FbReports { get; set; }
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
    }
}
