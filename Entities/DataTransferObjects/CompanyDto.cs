using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<CruiseCompany> CruiseCompanies { get; set; }
        //public virtual ICollection<Hotel> Hotels { get; set; }
        //public virtual ICollection<Outlet> Outlets { get; set; }
    }
}
