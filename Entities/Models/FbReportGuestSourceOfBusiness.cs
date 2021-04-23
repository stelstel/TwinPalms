using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class FbReportGuestSourceOfBusiness
    {
        public int FbReportId { get; set; }
        public int GuestSourceOfBusinessId { get; set; }

        // Navigation properties
        public virtual FbReport FbReport { get; set; }
        public virtual GuestSourceOfBusiness GuestSourceOfBusiness { get; set; }
    }
}
