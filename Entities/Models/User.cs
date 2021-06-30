using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            OutletUsers = new HashSet<OutletUser>();
            HotelUsers = new HashSet<HotelUser>();
            CompanyUsers = new HashSet<CompanyUser>();
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
        public virtual ICollection<RoomReport> RoomReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserRole> UserRoles { get; set; }
        /*[JsonIgnore]*/
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
    }
}
