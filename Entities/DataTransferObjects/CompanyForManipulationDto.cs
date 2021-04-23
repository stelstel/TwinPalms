using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class OutletForManipulationDto
    {
        [Required(ErrorMessage = "Outlet name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        // TODO
        //public int CompanyId { get; set; }

        //public virtual Company Company { get; set; }
        //public virtual ICollection<FbReport> FbReports { get; set; }
        //public virtual ICollection<OutletUser> OutletUsers { get; set; }

    }
}
