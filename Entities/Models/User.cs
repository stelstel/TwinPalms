using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();
            OutletUsers = new HashSet<OutletUser>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<FbReport> FbReports { get; set; }
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        public virtual ICollection<OtherReport> OtherReports { get; set; }
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
        public virtual ICollection<RoomsReport> RoomsReports { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
    }
}
