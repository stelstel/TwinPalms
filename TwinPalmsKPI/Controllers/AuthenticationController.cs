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
        public async Task<IActionResult> Authenticate([FromBody] UserForRegistrationDto userForRegistration)
        {
           
              
            var user = _mapper.Map<User>(userForRegistration);
            _logger.LogDebug("id  " + user.Id);
            foreach (var companyId in userForRegistration.Companies)
            {
                _logger.LogDebug("company " + companyId);

            }
    
            
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

            //var _user = await _userManager.FindByNameAsync(user.UserName);
            var _user = await _repository.User.GetUserAsync(user.Id, trackChanges: true);

            
            var roles = new string[] { "Basic", "Admin", "SuperAdmin" };
           
            await _userManager.AddToRolesAsync(user, roles);
           
            if (userForRegistration.Role == "Basic")
            {
                await _userManager.RemoveFromRolesAsync(user, new string[] { "SuperAdmin", "Admin" });
                //_repository.User.AddUserConnectionsAsync(user.Id, userForRegistration.Outlets.ToArray(), userForRegistration.Hotels.ToArray(), trackChanges: true);
                foreach (var hotelId in userForRegistration.Hotels)
                {
                    _user.HotelUsers.Add(new HotelUser { HotelId = hotelId, UserId = user.Id });
                }
                foreach (int outletId in userForRegistration.Outlets)
                {
                    _user.OutletUsers.Add(new OutletUser { OutletId = outletId, UserId = user.Id });
                }
            }
            else
            {
                //_logger.LogDebug("savin CompanyUsers ......");
                await _userManager.RemoveFromRoleAsync(user, "SuperAdmin");
                 /*var resp = await _repository.User.AddUserConnectionsAsync(user.Id, userForRegistration.Companies.ToArray(), trackChanges: true);
                 _logger.LogDebug("resp " + resp);*/

                foreach (int companyId in userForRegistration.Companies)
                {

                    user.CompanyUsers.Add(new CompanyUser { CompanyId = companyId, UserId = user.Id });

                }
            }
            await _repository.SaveAsync();

            // This can be used if we want to send the password directly by email
            // and use a token instead
            //var token = await _userManager.GeneratePasswordResetTokenAsync(user);
           /* var emailAddress = !string.IsNullOrWhiteSpace(userForRegistration.NotificationEmail) ? userForRegistration.NotificationEmail
                : user.Email;
        
            var message = new Message(new string[] { emailAddress }, "Welcome", "Your username is " + user.UserName + " and password is " + password);
            _emailSender.SendEmail(message);*/

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
            else
            {
                //_logger.LogDebug("user firstname" + _user.FirstName);
                var token = await _authManager.CreateToken();

                User _user = await _userManager.FindByNameAsync(user.UserName);
                IList<string> roles = _userManager.GetRolesAsync(_user).Result;
                User userDb = await _repository.User.GetUserAsync(_user.Id, trackChanges: false, roles);
                              
                var authUser = _mapper.Map<UserForLoginDto>(userDb);
                authUser.Token = token;

                return Ok(authUser);
            }
        }       //TODO older login response below- delete? (Anette)
                
        ////var userId = _user.Identity(FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                //var roles = _userManager.GetRolesAsync(_user).Result;

                //    /*var claimsRoles = User.Claims.Where(claim => claim.Type == "Role")
                //.Select(claim => claim.Value).ToArray();*/

                //if (!roles.Contains("SuperAdmin"))
                //{
                //    if (roles.Contains("Admin"))
                //    {
                //        return Ok(new
                //        {
                //            Token = token,
                //            Companies = _repository.User.GetCompaniesAsync(_user.Id, false)

                //        });
                //    }
                //    else
                //    {
                //        return Ok(new
                //        {
                //            Token = token,
                //            Outlets = _repository.User.GetOutletsAsync(_user.Id, false)

                //        });
                //    }
                //}
                //else
                //{
                //    return Ok(new
                //    {
                //        Token = token,
                //    });
                //}
            //}
        //}

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordInput)
        {
            
            var user = await _userManager.FindByEmailAsync(forgotPasswordInput.Email);
            if (user == null)
                return NotFound();

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var message = new Message(new string[] { user.Email }, "Reset password link",
$"<h3>Reset password</h3><a href=https://localhost:3000/reset-password?token={token}>Click on this link to reset your password</a>");
            _emailSender.SendEmail(message);

            return Ok();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto input, [FromQuery] string token)
        {
            
            if (!ModelState.IsValid)
                return BadRequest();
            var email = input.Email;
            _logger.LogDebug("Enail: " + email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return NotFound(email);

            var resetPassResult = await _userManager.ResetPasswordAsync(user, token, input.Password);
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
            //return Ok(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (!ModelState.IsValid)
                return BadRequest();
            if(User.Identity.IsAuthenticated)
            {
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByIdAsync(userId);

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

            }
            return Ok(201);
        }
    }
}
