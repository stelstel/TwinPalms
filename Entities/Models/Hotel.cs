using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Entities.Models
{
    public partial class Hotel
    {
        public Hotel()
        {
            HotelUsers = new HashSet<HotelUser>();
            RoomTypes = new HashSet<RoomType>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<RoomType> RoomTypes { get; set; }
    }
}
