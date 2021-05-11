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
    public class FbReportRepository : RepositoryBase<FbReport>, IFbReportRepository
    {
        public FbReportRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateFbReport(FbReport fbReport) => Create(fbReport);

        public void DeleteFbReport(FbReport fbReport)
        {
            Delete(fbReport);
        }

        public void UpdateFbReport(FbReport fbReport)
        {
            Update(fbReport);
        }

        public async Task<IEnumerable<FbReport>> GetAllFbReportsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderByDescending(o => o.Date)
            .ToListAsync();

        public async Task<IEnumerable<FbReport>> GetAllOutletFbReportsForOutlets(int[] outletIds, DateTime fromDate, DateTime toDate, bool trackChanges) =>
            await FindByCondition(fbr => outletIds.Contains(fbr.OutletId) && fbr.Date >= fromDate && fbr.Date <= toDate, trackChanges)
            .Include(fbr => fbr.FbReportGuestSourceOfBusinesses) // Include guestSourceOFBusiness junction table
                .ThenInclude(gsb => gsb.GuestSourceOfBusiness)
            .Include(fbr => fbr.WeatherFbReports) // Include weather junction table
                .ThenInclude(w => w.Weather)
            .OrderByDescending(o => o.Date)
            .ToListAsync();

        public async Task<FbReport> GetFbReportAsync(int id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges)
            .Include(fbr => fbr.FbReportGuestSourceOfBusinesses) // Include guestSourceOFBusiness junction table
                .ThenInclude(gsb => gsb.GuestSourceOfBusiness)
            .Include(fbr => fbr.WeatherFbReports) // Include weather junction table
                .ThenInclude(w => w.Weather)
            .Include(fbr => fbr.Image)
            .SingleOrDefaultAsync();
    }
}

