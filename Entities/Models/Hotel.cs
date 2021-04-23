using System;
using System.Collections.Generic;

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
        public virtual Company Company { get; set; }
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
    }
}
