using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class CruiseShipDto
    {
        // TODO
        //public Outlet()
        //{
        //    FbReports = new HashSet<FbReport>();
        //    OutletUsers = new HashSet<OutletUser>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int CruiseCompanyId { get; set; }

        public virtual CruiseCompany CruiseCompany { get; set; }
        public virtual ICollection<OtherReport> OtherReports { get; set; }
    }
}
