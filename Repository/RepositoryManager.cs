using Contracts;
using Entities;
using System;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private RepositoryContext _repositoryContext;
        private ICompanyRepository _companyRepository;
        private IOutletRepository _outletRepository;
        private IHotelRepository _hotelRepository;
        private ICruiseShipRepository _cruiseShipRepository;
        private ICruiseCompanyRepository _cruiseCompanyRepository;
        private IFbReportRepository _fbReportRepository;
        private ILocalEventRepository _localEventRepository;
        private IUserRepository _userRepository;
        private IGuestSourceOfBusinessRepository _guestSourceOfBusinessRepository;
        private IOtherReportRepository _otherReportRepository;
        private IRoomsReportRepository _roomsReportRepository;
        private IRoomTypeRepository _roomTypeRepository;
        private IWeatherRepository _weatherRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ICompanyRepository Company
        {
            get
            {
                if (_companyRepository == null)
                    _companyRepository = new CompanyRepository(_repositoryContext);

                return _companyRepository;
            }
        }

        public ICruiseCompanyRepository CruiseCompany
        {
            get
            {
                if (_cruiseCompanyRepository == null)
                    _cruiseCompanyRepository = new CruiseCompanyRepository(_repositoryContext);

                return _cruiseCompanyRepository;
            }
        }
        public ICruiseShipRepository CruiseShip
        {
            get
            {
                if (_cruiseShipRepository == null)
                    _cruiseShipRepository = new CruiseShipRepository(_repositoryContext);

                return _cruiseShipRepository;
            }
        }

        public IOutletRepository Outlet
        {
            get
            {
                if (_outletRepository == null)
                    _outletRepository = new OutletRepository(_repositoryContext);

                return _outletRepository;
            }
        }
        public IHotelRepository Hotel
        {
            get
            {
                if (_hotelRepository == null)
                    _hotelRepository = new HotelRepository(_repositoryContext);

                return _hotelRepository;
            }
        }

        public IUserRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new UserRepository(_repositoryContext);

                return _employeeRepository;
            }
        }

        public IFbReportRepository FbReport
        {
            get
            {
                if (_fbReportRepository == null)
                    _fbReportRepository = new FbReportRepository(_repositoryContext);

                return _fbReportRepository;
            }
        }

        

        public ILocalEventRepository LocalEvent
        {
            get
            {
                if (_localEventRepository == null)
                    _localEventRepository = new LocalEventRepository(_repositoryContext);

                return _localEventRepository;
            }
        }

        public IGuestSourceOfBusinessRepository GuestSourceOfBusiness
        {
            get => _guestSourceOfBusinessRepository ??= new GuestSourceOfBusinessRepository(_repositoryContext);            
        }

        public IOtherReportRepository OtherReport
        {
            get => _otherReportRepository ??= new OtherReportRepository(_repositoryContext);
        }
        
        public IRoomTypeRepository RoomType
        {
            get => _roomTypeRepository ??= new RoomTypeRepository(_repositoryContext);
        }
        public IWeatherRepository Weather
        {
            get => _weatherRepository ??= new WeatherRepository(_repositoryContext);
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
