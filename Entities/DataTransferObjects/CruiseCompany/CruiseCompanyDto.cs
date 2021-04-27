using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CruiseCompanyDto
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

        public virtual Company Company { get; set; }
        public virtual ICollection<FbReport> FbReports { get; set; }
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
    }
}
