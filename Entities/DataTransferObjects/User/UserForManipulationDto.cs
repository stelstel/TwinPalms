using Entities.Models;
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
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; }
        public string NotificationEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public ICollection<int> Outlets { get; set; }
        public ICollection<int> Hotels { get; set; }
        // TODO nullable Company id for admin users
        public ICollection<int> Companies { get; set; }
    }
}
