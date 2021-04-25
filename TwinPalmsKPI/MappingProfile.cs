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
                
            CreateMap<Employee, EmployeeDto>();

            CreateMap<CompanyForCreationDto, Company>();

            CreateMap<EmployeeForCreationDto, Employee>();

            CreateMap<EmployeeForUpdateDto, Employee>();

            CreateMap<CompanyForUpdateDto, Company>();
            
            CreateMap<UserForRegistrationDto, User>();

            CreateMap<Outlet, OutletDto>();

            CreateMap<OutletForCreationDto, Outlet>();

            CreateMap<OutletForUpdateDto, Outlet>();
            
            CreateMap<CruiseCompany, CruiseCompanyDto>();

            CreateMap<CruiseCompanyForCreationDto, CruiseCompany>();

            CreateMap<CruiseCompanyForUpdateDto, CruiseCompany>();
            
            CreateMap<CruiseShip,CruiseShipDto>();

            CreateMap<CruiseShipForCreationDto, CruiseShip>();

            CreateMap< CruiseShipForUpdateDto, CruiseShip>();
            
            CreateMap<FbReport, FbReportDto>();

            CreateMap<FbReportForCreationDto, FbReport>();

            CreateMap<FbReportForUpdateDto, FbReport>();

        }
    }
}
