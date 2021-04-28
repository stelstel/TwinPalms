﻿    using Contracts;
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
    public class OtherReportRepository : RepositoryBase<OtherReport>, IOtherReportRepository
    {
        public OtherReportRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateOtherReport(OtherReport otherReport) => Create(otherReport);

        public void DeleteOtherReport(OtherReport otherReport) => Delete(otherReport);

        public void UpdateOtherReport(OtherReport otherReport) => Update(otherReport);

        public async Task<IEnumerable<OtherReport>> GetAllOtherReportsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderByDescending(o => o.Date)
            .ToListAsync();

        public async Task<OtherReport> GetOtherReportAsync(int id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}
