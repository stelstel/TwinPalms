using AutoMapper;
using TwinPalmsKPI.ActionFilters;
using TwinPalmsKPI.Extensions;
using Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TwinPalmsKPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureSqlContext(Configuration);
            services.ConfigureRepositoryManager();
            services.AddAutoMapper(typeof(Startup));
            services.ConfigureSwagger();

            services.AddAuthentication(); 
            services.ConfigureIdentity();
            services.ConfigureJWT(Configuration);
            services.AddScoped<ValidationFilterAttribute>(); 
            services.AddScoped<ValidateCompanyExistsAttribute>();
            services.AddScoped<ValidateOutletExistsAttribute>();
            services.AddScoped<ValidateCruiseCompanyExistsAttribute>();
            services.AddScoped<ValidateCruiseShipExistsAttribute>();
            services.AddScoped<ValidateHotelExistsAttribute>();
            services.AddScoped<ValidateFbReportExistsAttribute>();
            services.AddScoped<ValidateEmployeeForCompanyAttribute>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>(); // TODO: Is this correct?

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Redwood API v1");
            });
            app.ConfigureExceptionHandler(logger);
            app.UseStaticFiles();

            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
