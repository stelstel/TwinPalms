using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TwinPalmsKPI
{
    public class MappingProfile : Profile
    {
       
        public MappingProfile()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<Company, CompanyUserDto>();
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();

            CreateMap<Hotel, HotelDto>();
            CreateMap<Hotel, HotelUserDto>(); 
            CreateMap<HotelForCreationDto, Hotel>();
            CreateMap<HotelForUpdateDto, Hotel>();

            CreateMap<Outlet, OutletDto>();       
            CreateMap<Outlet, OutletUserDto>();
            CreateMap<OutletForCreationDto, Outlet>();
            CreateMap<OutletForUpdateDto, Outlet>();

            CreateMap<User, UserDto>()              
                .ForMember(dto => dto.Roles, user => user.MapFrom(user => user.UserRoles.Select(ur => ur.Role.Name).ToList()))

                // Properties are mapped differently depending on the users role.
                
                .ForMember(dto => dto.Companies, opt =>
                {
                    // Only for admin users                    
                    opt.Condition(src => src.UserRoles.Any(ur => ur.Role.Name.Contains("Admin")) && !src.UserRoles.Any(ur => ur.Role.Name.Contains("SuperAdmin")));
                    opt.MapFrom(user => user.CompanyUsers.Select(cu => cu.Company).ToList());
                })
                
                .ForMember(dto => dto.Hotels, opt =>
                    {
                        // Only for basic users
                        opt.PreCondition(src => !src.UserRoles.Any(ur => ur.Role.Name.EndsWith("Admin")));                        
                        opt.MapFrom(user => user.HotelUsers.Select(hu => hu.Hotel).ToList());                       
                    })
                .ForMember(dto => dto.Outlets, opt =>
                    {
                        // Only for basic users
                        opt.PreCondition(src => !src.UserRoles.Any(ur => ur.Role.Name.EndsWith("Admin")));
                        opt.MapFrom(user => user.OutletUsers.Select(ou => ou.Outlet).ToList());
                    });                                   
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<User, UserForLoginDto>()

                //Preconditions so destination not displays empty arrays due to source members containing null             
                .ForMember(dto => dto.Companies, opt =>
                { 
                
                    opt.PreCondition(src => src.CompanyUsers != null && src.CompanyUsers.Count > 0);
                    opt.MapFrom(user => user.CompanyUsers.Select(cu => cu.Company).ToList());
                    
                })
                .ForMember(dto => dto.Hotels, opt =>
                {
                    opt.PreCondition(src => src.HotelUsers != null && src.HotelUsers.Count > 0);
                    opt.MapFrom(user => user.HotelUsers.Select(hu => hu.Hotel).ToList());
                })
                .ForMember(dto => dto.Outlets, opt =>
                {
                    opt.PreCondition(src => src.OutletUsers != null && src.OutletUsers.Count > 0);
                    opt.MapFrom(user => user.OutletUsers.Select(ou => ou.Outlet).ToList());
                });


            CreateMap<UserForUpdateDto, User>();
                                 
            CreateMap<CruiseCompany, CruiseCompanyDto>();
            CreateMap<CruiseCompanyForCreationDto, CruiseCompany>();
            CreateMap<CruiseCompanyForUpdateDto, CruiseCompany>();
                        
            CreateMap<CruiseShip,CruiseShipDto>();
            CreateMap<CruiseShipForCreationDto, CruiseShip>();
            CreateMap<CruiseShipForUpdateDto, CruiseShip>();
            
            CreateMap<RoomsReport, RoomsReportDto>();
            CreateMap<RoomsReportForCreationDto, RoomsReport>();
            CreateMap<RoomsReportForUpdateDto, RoomsReport>();

            CreateMap<FbReport, FbReportDto>();
            CreateMap<FbReportForCreationDto, FbReport>();

            CreateMap<FbReportForUpdateDto, FbReport>();

            CreateMap<OtherReport, OtherReportDto>();
            CreateMap<OtherReportForCreationDto, OtherReport>();
            CreateMap<OtherReportForUpdateDto, OtherReport>();

            CreateMap<LocalEvent, LocalEventDto>();
            CreateMap<LocalEventForCreationDto, LocalEvent>();
            CreateMap<LocalEventForUpdateDto, LocalEvent>();

            CreateMap<GuestSourceOfBusiness, GuestSourceOfBusinessDto>();
            CreateMap<GuestSourceOfBusinessForCreationDto, GuestSourceOfBusiness>();
            CreateMap<GuestSourceOfBusinessForUpdateDto, GuestSourceOfBusiness>();

            CreateMap<Weather, WeatherDto>();
            CreateMap<WeatherForCreationDto, Weather>();
            CreateMap<WeatherForUpdateDto, Weather>();
        }
    }
}
