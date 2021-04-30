using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

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

            CreateMap<User, UserDto>();
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
            CreateMap<RoomsReportForCreationDto, FbReport>();
            CreateMap<RoomsReportForUpdateDto, FbReport>();
                        
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
        }
    }
}
