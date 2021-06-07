
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    //Dto for entity
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public IEnumerable<OutletDto> Outlets { get; set; }
    }
}
