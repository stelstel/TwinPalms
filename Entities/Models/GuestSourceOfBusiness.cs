using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;


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

        // Navigation property
        [JsonIgnore]
        public virtual ICollection<FbReportGuestSourceOfBusiness> FbReportGuestSourceOfBusinesses { get; set; }
    }
}
