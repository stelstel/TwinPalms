using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class LocalEvent
    {
        public int Id { get; set; }
        public string Event { get; set; }
        public bool Active { get; set; }
    }
}
