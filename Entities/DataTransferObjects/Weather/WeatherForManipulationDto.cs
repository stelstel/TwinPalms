using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public abstract class WeatherForManipulationDto
    {
        [Required(ErrorMessage = "Name of weathertype is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string TypeOfWeather { get; set; }
    }
}
