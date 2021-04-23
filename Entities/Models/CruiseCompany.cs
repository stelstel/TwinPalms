using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class CruiseCompany
    {
        public CruiseCompany()
        {
            CruiseShips = new HashSet<CruiseShip>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<CruiseShip> CruiseShips { get; set; }
    }
}
