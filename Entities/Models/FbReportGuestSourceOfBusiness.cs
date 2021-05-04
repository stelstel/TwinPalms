using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.Models
{
    public partial class FbReportGuestSourceOfBusiness
    {
        public int FbReportId { get; set; }
        public int GuestSourceOfBusinessId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual FbReport FbReport { get; set; }
        [JsonIgnore]
        public virtual GuestSourceOfBusiness GuestSourceOfBusiness { get; set; }
    }
}
