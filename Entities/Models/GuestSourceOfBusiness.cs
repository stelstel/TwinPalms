using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class GuestSourceOfBusiness
    {
        public GuestSourceOfBusiness()
        {
           // FbReportGuestSourceOfBusinesses = new HashSet<FbReportGuestSourceOfBusiness>();
        }

        public int Id { get; set; }
        public string SourceOfBusiness { get; set; }
        
        // TODO: Navigation property
       // public virtual ICollection<FbReportGuestSourceOfBusiness> FbReportGuestSourceOfBusinesses { get; set; }
    }
}
