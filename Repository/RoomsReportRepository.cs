﻿using Contracts;
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
    public class RoomsReportRepository : RepositoryBase<RoomsReport>, IRoomsReportRepository
    {
        public RoomsReportRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateRoomsReport(RoomsReport roomsReport) => Create(roomsReport);

        public void DeleteRoomsReport(RoomsReport roomsReport) => Delete(roomsReport);

        public void UpdateRoomsReport(RoomsReport roomsReport) => Update(roomsReport);

        public async Task<IEnumerable<RoomsReport>> GetAllRoomsReportsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.RoomTypeId) //sorts by roomtype, which is assumed unique between hotels
            .ToListAsync();

        public async Task<RoomsReport> GetRoomsReportAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}