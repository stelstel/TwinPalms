using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public abstract class UserForManipulationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        // We will try to set a random password during registration
        //[Required(ErrorMessage = "Password is required")]
        //public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<string> Roles { get; set; }
        public ICollection<int> Outlets { get; set; }
        // TODO nullable Company id for admin users
        //public int? CompanyId { get; set; }
    }
}
