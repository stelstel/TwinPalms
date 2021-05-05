using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRoomsReportRepository
    {
        Task<IEnumerable<RoomsReport>> GetAllRoomsReportsAsync(bool trackChanges);
        Task<IEnumerable<RoomsReport>> GetAllRoomsReportsDataAsync(int hotelId, int[] roomTypes, DateTime fromDate, DateTime toDate, bool trackChanges);
        Task<RoomsReport> GetRoomsReportAsync(int id, bool trackChanges);

        void CreateRoomsReport(RoomsReport roomsReport);
        void DeleteRoomsReport(RoomsReport roomsReport);
        void UpdateRoomsReport(RoomsReport roomsReport);
    }
}
