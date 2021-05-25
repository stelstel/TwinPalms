using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Entities.DataTransferObjects
{
    public abstract class LocalEventForManipulationDto
    {
        [Required(ErrorMessage = "Event is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Event is 60 characters.")]
        public string Event { get; set; }
        public bool Active { get; set; }
    }
}
