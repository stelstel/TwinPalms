using Entities.Models;
using System;
using System.Collections.Generic;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Role> Roles { get; set; }
        public ICollection<Outlet> Outlets { get; set; }
    }
}
