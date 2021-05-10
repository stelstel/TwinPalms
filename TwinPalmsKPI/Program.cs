using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace TwinPalmsKPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

                var services = host.Services.CreateScope().ServiceProvider;
                   /*var context = services.GetRequiredService<RepositoryContext>();
                    context.Database.EnsureCreated();*/
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    await ContextSeed.AddUserPasswordAsync(userManager);

            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                    Host.CreateDefaultBuilder(args)
                        .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                            webBuilder.UseUrls("http://*:" + Environment.GetEnvironmentVariable("PORT"));
                            
                      });
    }
}
    