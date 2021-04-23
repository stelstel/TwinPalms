using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    //[Index(nameof(Name), IsUnique = true)]
    public partial class RoomType
    {
        public RoomType()
        {
            RoomsReports = new HashSet<RoomsReport>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<RoomsReport> RoomsReports { get; set; }
    }
}
