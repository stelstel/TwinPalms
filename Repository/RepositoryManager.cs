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
        private IEmployeeRepository _employeeRepository;
        private IOutletRepository _outletRepository;
        private IHotelRepository _hotelRepository;
        private ICruiseShipRepository _cruiseShipRepository;
        private ICruiseCompanyRepository _cruiseCompanyRepository;

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

        public IEmployeeRepository Employee
        {
            get
            {
                if (_employeeRepository == null)
                    _employeeRepository = new EmployeeRepository(_repositoryContext);

                return _employeeRepository;
            }
        }

        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();
    }
}
