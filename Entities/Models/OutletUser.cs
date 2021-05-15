using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class OutletUser
    {
        public string UserId { get; set; }
        public int OutletId { get; set; }
        [JsonIgnore]
        public virtual Outlet Outlet { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
