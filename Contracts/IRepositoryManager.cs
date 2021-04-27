﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IOutletRepository Outlet { get; }
        IHotelRepository Hotel { get; }
        ICruiseCompanyRepository CruiseCompany { get; }
        ICruiseShipRepository CruiseShip { get; }
        IFbReportRepository FbReport { get; }
        IGuestSourceOfBusinessRepository GuestSourceOfBusinessRepository { get; }       
        ILocalEventRepository LocalEvent { get; }
        IOtherReportRepository OtherReport { get;}       
        IRoomsReportRepository RoomsReportRepository { get; }
        IRoomTypeRepository RoomTypeRepository { get; }
        IWeatherRepository WeatherRepository { get; }

        Task SaveAsync();
    }
}
