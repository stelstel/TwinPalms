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
            CreateMap<CompanyForCreationDto, Company>();
            CreateMap<CompanyForUpdateDto, Company>();

            CreateMap<Hotel, HotelDto>();
            CreateMap<HotelForCreationDto, Hotel>();
            CreateMap<HotelForUpdateDto, Hotel>();



            CreateMap<User, UserDto>()
                .ForMember(dto => dto.Hotels, user => user.MapFrom(user => user.HotelUsers.Select(hu => hu.Hotel).ToList()))
                .ForMember(dto => dto.Outlets, user => user.MapFrom(user => user.OutletUsers.Select(ou => ou.Outlet).ToList()))
                .ForMember(dto => dto.Roles, user => user.MapFrom(user => user.UserRoles.Select(ur => ur.Role.Name).ToList()));
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<UserForUpdateDto, User>();
            

            CreateMap<Outlet, OutletDto>();
            CreateMap<OutletForCreationDto, Outlet>();
            CreateMap<OutletForUpdateDto, Outlet>();
                        
            CreateMap<CruiseCompany, CruiseCompanyDto>();
            CreateMap<CruiseCompanyForCreationDto, CruiseCompany>();
            CreateMap<CruiseCompanyForUpdateDto, CruiseCompany>();
                        
            CreateMap<CruiseShip,CruiseShipDto>();
            CreateMap<CruiseShipForCreationDto, CruiseShip>();
            CreateMap<CruiseShipForUpdateDto, CruiseShip>();
            
            CreateMap<RoomsReport, RoomsReportDto>();
            //CreateMap<RoomsReportDto, RoomsReport>();
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
