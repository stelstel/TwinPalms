using Entities.Configuration;
using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());

            modelBuilder.ApplyConfiguration(new GuestSourceOfBusinessConfiguration());
            modelBuilder.ApplyConfiguration(new FbReportGuestSourceOfBusinessConfiguration());
            modelBuilder.ApplyConfiguration(new HotelUserConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherFbReportConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherOtherReportConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherRoomsReportConfiguration());


        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public virtual DbSet<GuestSourceOfBusiness> GuestSourceOfBusinesses { get; set; }

        
        public virtual DbSet<CruiseCompany> CruiseCompanies { get; set; }
        public virtual DbSet<CruiseShip> CruiseShips { get; set; }
        public virtual DbSet<FbReport> FbReports { get; set; }
        public virtual DbSet<Hotel> Hotels { get; set; }
        public virtual DbSet<HotelUser> HotelUsers { get; set; }
        public virtual DbSet<LocalEvent> LocalEvents { get; set; }
        public virtual DbSet<OtherReport> OtherReports { get; set; }
        public virtual DbSet<Outlet> Outlets { get; set; }
        public virtual DbSet<OutletUser> OutletUsers { get; set; }
       
        public virtual DbSet<RoomType> RoomTypes { get; set; }
        public virtual DbSet<RoomsReport> RoomsReports { get; set; }                
       
        public virtual DbSet<Weather> Weathers { get; set; }
        public virtual DbSet<WeatherFbReport> WeatherFbReports { get; set; }
        public virtual DbSet<WeatherOtherReport> WeatherOtherReports { get; set; }
        public virtual DbSet<WeatherRoomsReport> WeatherRoomsReports { get; set; }
        public virtual DbSet<FbReportGuestSourceOfBusiness> FbReportGuestSourceOfBusinesses { get; set; }
        public object FbReportGuestSourceOfBusiness { get; internal set; }
    }
}
