using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DataTransferObjects
{
    public class FbReportForCreationDto : FbReportForManipulationDto
    {
        [Required]
        public ICollection<GsobDto> GuestSourceOfBusinesses { get; set; }
    }
}
   