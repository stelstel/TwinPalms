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
            await userManager.AddToRolesAsync(superAdmin, new string[] { "SuperAdmin", "Admin", "Basic" });

            var admin = userManager.FindByEmailAsync("ADMIN@TWINPALMS").Result;
            await userManager.AddPasswordAsync(admin, "qwert12345");
            await userManager.AddToRolesAsync(admin, new string[] { "Admin", "Basic" });
            
            var admin2 = userManager.FindByEmailAsync("ADMIN2@TWINPALMS").Result;
            await userManager.AddPasswordAsync(admin2, "qwert12345");
            await userManager.AddToRolesAsync(admin2, new string[] { "Admin", "Basic" });

            var basic1 = userManager.FindByEmailAsync("BASIC2@TWINPALMS").Result;
            await userManager.AddPasswordAsync(basic1, "qwert12345");
            await userManager.AddToRoleAsync(basic1, "Basic");

            var basic2 = userManager.FindByEmailAsync("BASIC1@TWINPALMS").Result;
            await userManager.AddPasswordAsync(basic2, "qwert12345");
            await userManager.AddToRoleAsync(basic2, "Basic");




        }

    }
}
