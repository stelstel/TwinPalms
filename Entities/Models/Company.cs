using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {
        public Company()
        {
            CruiseCompanies = new HashSet<CruiseCompany>();
            Hotels = new HashSet<Hotel>();
            Outlets = new HashSet<Outlet>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        // Navigation properties
        public virtual ICollection<CruiseCompany> CruiseCompanies { get; set; }
        public virtual ICollection<Hotel> Hotels { get; set; }
        public virtual ICollection<Outlet> Outlets { get; set; }
        public virtual ICollection<CompanyUser> CompanyUsers { get; set; }
    }
}
