using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRoomsReportRepository
    {
        Task<IEnumerable<RoomReport>> GetAllRoomsReportsAsync(bool trackChanges);
        Task<IEnumerable<RoomReport>> GetAllRoomsReportsDataAsync(int hotelId, int[] roomTypes, DateTime fromDate, DateTime toDate, bool trackChanges);
        Task<RoomReport> GetRoomsReportAsync(int id, bool trackChanges);

        void CreateRoomsReport(RoomReport roomsReport);
        void DeleteRoomsReport(RoomReport roomsReport);
        void UpdateRoomsReport(RoomReport roomsReport);
    }
}
