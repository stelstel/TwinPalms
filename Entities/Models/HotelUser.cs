using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class HotelUser
    {
        public int UserId { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual User User { get; set; }
    }
}
