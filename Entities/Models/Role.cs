using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Role : IdentityRole
    {
        /*[JsonIgnore]*/
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
