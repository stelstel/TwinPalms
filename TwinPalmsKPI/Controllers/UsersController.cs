using AutoMapper;
using TwinPalmsKPI.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TwinPalmsKPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
       

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper )
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;           
        }

        /// <summary>
        /// Gets a list of all users
        /// </summary>
        [HttpGet(Name = "GetUsers"), Authorize(Roles="SuperAdmin")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.User.GetUsersAsync(trackChanges: false);           
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            
            return Ok(userDtos);
        }

        /// <summary>
        /// Gets user by ID
        /// </summary>
        [HttpGet("{id}", Name = "UserById"), Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetUser(string id)
        {          
            var userDb = await _repository.User.GetUserAsync(id, trackChanges: false);
            if (userDb == null)
            {
                _logger.LogInfo($"User with id {id} doesn't exist in the database.");
                return NotFound();
            }

            var user = _mapper.Map<UserDto>(userDb);
            return Ok(user);
        }

        /// <summary>
        /// Deletes user by ID
        /// </summary>
        [HttpDelete("{id}"), Authorize(Roles = "SuperAdmin")]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = HttpContext.Items["user"] as User;

            _repository.User.DeleteUser(user);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates user by ID
        /// </summary>
        [HttpPut("{id}"), Authorize(Roles = "SuperAdmin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDto user)
        {
            var userEntity = HttpContext.Items["user"] as User;
            
            if (user.Role == "Basic")
            {
                userEntity.OutletUsers.Clear();
                userEntity.HotelUsers.Clear();
                
                foreach (var outletId in user.Outlets)
                    userEntity.OutletUsers.Add(new OutletUser { OutletId = outletId, UserId = id });
                

                foreach (var hotelId in user.Hotels)
                    userEntity.HotelUsers.Add(new HotelUser { HotelId = hotelId, UserId = id });
                    
                
            } 
            else
            {
                userEntity.CompanyUsers.Clear();
                foreach (var companyId in user.Companies)
                    userEntity.CompanyUsers.Add(new CompanyUser { CompanyId = companyId, UserId = id });
            }
            _repository.User.UpdateUser(userEntity);
            _mapper.Map(user, userEntity);
            await _repository.SaveAsync();
            var userToReturn = _mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("UserById", new { id = userToReturn.Id }, userToReturn);
        }
    }
}
