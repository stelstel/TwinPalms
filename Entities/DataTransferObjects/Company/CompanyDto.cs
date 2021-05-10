using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObjects
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }

    }
}
