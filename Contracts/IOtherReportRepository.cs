using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOtherReportRepository
    {
        Task<IEnumerable<OtherReport>> GetAllOtherReportsAsync(bool trackChanges);
        Task<OtherReport> GetOtherReportAsync(int id, bool trackChanges);

        void CreateOtherReport(OtherReport otherReport);
        void DeleteOtherReport(OtherReport otherReport);
        void UpdateOtherReport(OtherReport otherReport);
    }
}
