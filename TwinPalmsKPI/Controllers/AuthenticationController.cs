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
        private readonly IEmailSender _emailSender;

        public AuthenticationController(
            ILoggerManager logger, 
            IMapper mapper, 
            UserManager<User> userManager, 
            IAuthenticationManager authManager,
            IEmailSender emailSender)
            {
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

            var user = _mapper.Map<User>(userForRegistration);
            if (userForRegistration.Outlets.Count > 0)
            {
            foreach (int outletId in userForRegistration.Outlets)
            {
                var outletUser = new OutletUser
                {
                    OutletId = outletId, 
                    UserId = user.Id
                };
                user.OutletUsers.Add(outletUser);
            }
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
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var message = new Message(new string[] { user.Email }, "Set new password", "token = "+token);
            _emailSender.SendEmail(message);

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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordInput)
        {
            

            var user = await _userManager.FindByEmailAsync(forgotPasswordInput.Email);
            if (user == null)
                return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var message = new Message(new string[] { user.Email }, "Reset password link", "Link to client side reset password"+token);
            _emailSender.SendEmail(message);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordInput, [FromQuery] string token)
        {
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
    }
}
