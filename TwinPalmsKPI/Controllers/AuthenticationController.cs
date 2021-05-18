using AutoMapper;
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
using EmailService;
using Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TwinPalmsKPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoggerManager _logger; 
        private readonly IRepositoryManager _repository; 
        private readonly IMapper _mapper; 
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authManager;
        private readonly IEmailSender _emailSender;

        public AuthenticationController(
            IRepositoryManager repository,
            ILoggerManager logger, 
            IMapper mapper, 
            UserManager<User> userManager, 
            IAuthenticationManager authManager,
            IEmailSender emailSender)
            {
                _repository = repository;   
                _logger = logger; 
                _mapper = mapper; 
                _userManager = userManager; 
                _authManager = authManager;
                _emailSender = emailSender;
            }

        [HttpPost]  
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {

            var id = Guid.NewGuid().ToString();
            var user = _mapper.Map<User>(userForRegistration);
            user.Id = id;
            
            var password = Password.GenerateRandomPassword();
            await _userManager.AddPasswordAsync(user, password);
            
            var result = await _userManager.CreateAsync(user, password);
            
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            
            var roles = new string[] { "Basic", "Admin", "SuperAdmin" };
            await _userManager.AddToRolesAsync(user, roles);

            if (userForRegistration.Role == "Admin")
            {
                await _userManager.RemoveFromRoleAsync(user, "SuperAdmin");
                _repository.User.AddCompaniesAsync(user.Id, userForRegistration.Companies.ToArray(), true);
                
            }
            else
            {
                await _userManager.RemoveFromRolesAsync(user, new string[] { "SuperAdmin", "Admin" });

                _repository.User.AddOutletsAndHotelsAsync(user.Id, userForRegistration.Outlets.ToArray(), userForRegistration.Hotels.ToArray(), true);
                
            }



            // This can be used if we want to send the password directly by email
            // and use a token instead
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var message = new Message(new string[] { user.Email }, "Welcome", "Your username is " + user.UserName + " and password is " + password);
            _emailSender.SendEmail(message);

            return StatusCode(201);
            //return Ok();
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordInput)
        {
            

            var user = await _userManager.FindByEmailAsync(forgotPasswordInput.Email);
            if (user == null)
                return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var message = new Message(new string[] { user.Email }, "Reset password link", "<h3>Reset Email</h3><a href'https://localhost:5000/reset_password?token='"+token+">Click link</a>");
            _emailSender.SendEmail(message);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordInput, [FromQuery] string token)
        {
            _logger.LogInfo("Token: " +token);
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetPasswordInput.Email);
            if (user == null)
                return NotFound();

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, resetPasswordInput.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok(201);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto input)
        {
            return Ok(User.Identity);
            if (!ModelState.IsValid)
                return BadRequest();
            
            var user = await _userManager.FindByEmailAsync(User.FindFirst(ClaimTypes.Email).Value);
            if (user == null)
                return NotFound();
            
           var changePassword = await _userManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword);

            
            if (!changePassword.Succeeded)
            {
                foreach (var error in changePassword.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return BadRequest(ModelState);
            }

            return Ok(201);
        }
    }
}
