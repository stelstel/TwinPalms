using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class CompanyUser
    {
        public string UserId { get; set; }
        public int CompanyId { get; set; }

        // Navigation properties
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
