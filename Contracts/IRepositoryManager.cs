﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IUserRepository User { get; }
        IOutletRepository Outlet { get; }
        IHotelRepository Hotel { get; }
        ICruiseCompanyRepository CruiseCompany { get; }
        ICruiseShipRepository CruiseShip { get; }
        IFbReportRepository FbReport { get; }
        IGuestSourceOfBusinessRepository GuestSourceOfBusiness { get; }       
        ILocalEventRepository LocalEvent { get; }
        IOtherReportRepository OtherReport { get;}       
        IRoomsReportRepository RoomsReport { get; }
        IRoomTypeRepository RoomType { get; }
        IWeatherRepository Weather { get; }

        Task SaveAsync();
    }
}
