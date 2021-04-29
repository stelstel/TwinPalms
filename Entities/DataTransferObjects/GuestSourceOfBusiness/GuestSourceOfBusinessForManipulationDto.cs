using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class GuestSourceOfBusinessForManipulationDto
    {
        [Required(ErrorMessage = "SourceOfBusiness name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the SourceOfBusiness is 60 characters.")]
        public string SourceOfBusiness { get; set; }
    }
}
