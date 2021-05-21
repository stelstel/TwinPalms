using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class OutletDto
    {
        // TODO Unused. Delete? (Anette)
        //public Outlet()
        //{
        //    FbReports = new HashSet<FbReport>();
        //    OutletUsers = new HashSet<OutletUser>();
        //}

        public int Id { get; set; }
        public string Name { get; set; }
        public int CompanyId { get; set; }
        
        //TODO: Delete members in Dtos that are not used? (Anette)
        [JsonIgnore]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        public virtual ICollection<FbReport> FbReports { get; set; }
        [JsonIgnore]       
        public virtual ICollection<OutletUser> OutletUsers { get; set; }
    }
}
