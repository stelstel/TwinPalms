using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace TwinPalmsKPI
{
    public class ContextSeed
    {
        /*private readonly ILoggerManager _logger;
        public ContextSeed(ILoggerManager logger)
        {
            _logger = logger;
        }*/
        
        public static async Task AddUserPasswordAsync(UserManager<User> userManager)
        {

            var superAdmin = userManager.FindByEmailAsync("SUPERADMIN@TWINPALMS").Result;
            //var token = await userManager.GeneratePasswordResetTokenAsync(superAdmin);
            await userManager.AddPasswordAsync(superAdmin, "qwert12345");

            var admin = userManager.FindByEmailAsync("ADMIN@TWINPALMS").Result;
            await userManager.AddPasswordAsync(admin, "qwert12345");
            
            var basic1 = userManager.FindByEmailAsync("BASIC2@TWINPALMS").Result;
            await userManager.AddPasswordAsync(basic1, "qwert12345 ");

            var basic2 = userManager.FindByEmailAsync("BASIC1@TWINPALMS").Result;
            await userManager.AddPasswordAsync(basic2, "qwert12345");
            
           



        }

    }
}
