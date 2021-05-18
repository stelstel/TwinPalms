using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class HotelUser
    {
        public string UserId { get; set; }
        public int HotelId { get; set; }
        [JsonIgnore]
        public virtual Hotel Hotel { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
