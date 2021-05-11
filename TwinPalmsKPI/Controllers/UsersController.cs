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

namespace TwinPalmsKPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }
        /// <summary>
        /// Gets a list of all users
        /// </summary>
        [HttpGet(Name = "GetUsers")]
        public async Task<IEnumerable<UserDto>> GetUsers()
        {

            var users = await _userManager.Users
                 .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                 .Include(u => u.OutletUsers).ThenInclude(ou => ou.Outlet)
                 .Include(u => u.HotelUsers).ThenInclude(hu => hu.Hotel).ToListAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);
            
            return userDtos;


        }
        /// <summary>
        /// Gets user by ID
        /// </summary>
        [HttpGet("{id}", Name = "UserById")]
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
        /// Gets a list of all users
        /// </summary>
        /*[HttpGet(Name = "GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repository.User.GetUsersAsync(trackChanges: false);
            foreach (var user in users)
            {
               await _userManager.GetRolesAsync(user);
            }
            

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);


            return Ok(users); 
        }*/
        /// <summary>
        /// Deletes user by ID
        /// </summary>
        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = HttpContext.Items["user"] as User;

            _repository.User.DeleteUser(user);
            await _repository.SaveAsync();

            return NoContent();
        }
        /// <summary>
        /// Updates user by ID
        /// </summary>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateUserExistsAttribute))]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UserForUpdateDto user)
        {
            var userEntity = HttpContext.Items["user"] as User;
            _repository.User.UpdateUser(userEntity);
            _mapper.Map(user, userEntity);
            await _repository.SaveAsync();
            var userToReturn = _mapper.Map<UserDto>(userEntity);
            return CreatedAtRoute("UserById", new { id = userToReturn.Id }, userToReturn);
        }
    }
}
