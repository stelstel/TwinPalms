using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IFbReportRepository
    {
        Task<IEnumerable<FbReport>> GetAllFbReportsAsync(bool trackChanges);
        Task<FbReport> GetFbReportAsync(int id, bool trackChanges);
        void CreateFbReport(FbReport fbReport);
        void DeleteFbReport(FbReport fbReport);
        void UpdateFbReport(FbReport fbReport);
    }
}
