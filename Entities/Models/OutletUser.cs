using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class OutletUser
    {
        public string UserId { get; set; }
        public int OutletId { get; set; }

        public virtual Outlet Outlet { get; set; }
        public virtual User User { get; set; }
    }
}
