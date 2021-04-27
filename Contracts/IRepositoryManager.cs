using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        ICruiseCompanyRepository CruiseCompany { get; }
        ICruiseShipRepository CruiseShip { get; }
        IEmployeeRepository Employee { get; }
        IFbReportRepository FbReport { get; }
        IGuestSourceOfBusinessRepository GuestSourceOfBusinessRepository { get; }
        IHotelRepository Hotel { get; }
        ILocalEventRepository LocalEvent { get; }
        IOtherReportRepository OtherReport { get;}
        IOutletRepository Outlet { get; }
        IRoomsReportRepository RoomsReportRepository { get; }
        IRoomTypeRepository RoomTypeRepository { get; }
        IWeatherRepository WeatherRepository { get; }

        Task SaveAsync();
    }
}
