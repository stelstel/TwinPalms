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
        }
    }
}
