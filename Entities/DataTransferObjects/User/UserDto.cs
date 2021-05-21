using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.DataTransferObjects
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public string Email { get; set; }
        //public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<OutletUserDto> Outlets { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<HotelUserDto> Hotels { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IEnumerable<CompanyUserDto> Companies { get; set; }
    }
}
