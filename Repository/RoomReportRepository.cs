using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RoomReportRepository : RepositoryBase<RoomReport>, IRoomsReportRepository
    {
        public RoomReportRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateRoomsReport(RoomReport roomsReport) => Create(roomsReport);

        public void DeleteRoomsReport(RoomReport roomsReport) => Delete(roomsReport);

        public void UpdateRoomsReport(RoomReport roomsReport) => Update(roomsReport);

        public async Task<IEnumerable<RoomReport>> GetAllRoomsReportsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.RoomTypeId) //sorts by roomtype, which is assumed unique between hotels
            .ToListAsync();
        public async Task<IEnumerable<RoomReport>> GetAllRoomsReportsDataAsync(int hotelId, int[] roomTypes, DateTime fromDate, DateTime toDate, bool trackChanges) =>
            await FindAll(trackChanges)
            .Include(rr => rr.RoomType).ThenInclude(rt => rt.Hotel)
            .Where(rr => roomTypes.Contains(rr.RoomTypeId) && rr.Date >= fromDate && rr.Date <= toDate)
            .OrderBy(rr => rr.RoomType.HotelId)
            .ThenBy(rr => rr.Date) //sorts by roomtype, which is assumed unique between hotels
            .ToListAsync();
        public async Task<RoomReport> GetRoomsReportAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}
