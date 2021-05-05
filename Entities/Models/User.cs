using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<FbReport> FbReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<OtherReport> OtherReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoomsReport> RoomsReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        
    }
}
