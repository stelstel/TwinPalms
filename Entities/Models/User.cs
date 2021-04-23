using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<FbReport> FbReports { get; set; }
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        public virtual ICollection<OtherReport> OtherReports { get; set; }
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
        public virtual ICollection<RoomsReport> RoomsReports { get; set; }
        
    }
}
