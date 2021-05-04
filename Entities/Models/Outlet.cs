using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class Outlet
    {
        // TODO
        //public Outlet()
        //{
        //    FbReports = new HashSet<FbReport>();
        //    OutletUsers = new HashSet<OutletUser>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual ICollection<FbReport> FbReports { get; set; }
        [JsonIgnore]
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
    }
}
