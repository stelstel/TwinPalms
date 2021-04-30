﻿using AutoMapper;
using TwinPalmsKPI.ActionFilters;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPalmsKPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger; 
        private readonly IMapper _mapper; 
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        //private IUserRepository _userRepository;

        public AuthenticationController(
            ILoggerManager logger, 
            IMapper mapper, 
            UserManager<User> userManager, 
            IAuthenticationManager authManager
            /*IUserRepository userRepository*/)
            {
                _logger = logger; 
                _mapper = mapper; 
                _userManager = userManager; 
                _authManager = authManager;
                //_userRepository = userRepository;
            }

        [HttpPost]  
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {

            var user = _mapper.Map<User>(userForRegistration);
            
            foreach (int outletId in userForRegistration.Outlets)
            {
                var outletUser = new OutletUser
                {
                    OutletId = outletId,
                    UserId = user.Id
                };
                user.OutletUsers.Add(outletUser);
            }
            
            var result = await _userManager.CreateAsync(user, userForRegistration.Password); 
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            await _userManager.AddToRolesAsync(user, userForRegistration.Roles);
           
            
            return StatusCode(201);
        }
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _authManager.ValidateUser(user))
            {
                _logger.LogWarning($"{nameof(Authenticate)}: Authentication failed. Wrong user name or password."); 
                return Unauthorized();
            }
            return Ok(new {
                
                Token = await _authManager.CreateToken() });
        }
    }
}
