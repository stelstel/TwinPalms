using Microsoft.AspNetCore.Identity;

namespace Entities.Models
{
    public class UserRole : IdentityUserRole<string>
    {
        
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }

    }
}
