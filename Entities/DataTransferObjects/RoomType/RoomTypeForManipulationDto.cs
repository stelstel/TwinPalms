using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class RoomTypeForManipulationDto
    {
        [Required(ErrorMessage = "Name of roomtype is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }
        
        //HotelId (parent) is already set when user manipulates roomtype
    }
}
