using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class CruiseCompanyForManipulationDto
    {
        [Required(ErrorMessage = "Cruise company is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
        public int CompanyId { get; set; }
    }
}
