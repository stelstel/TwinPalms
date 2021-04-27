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
    public class CruiseShipRepository : RepositoryBase<CruiseShip>, ICruiseShipRepository
    {
        public CruiseShipRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCruiseShip(CruiseShip cruiseShip) => Create(cruiseShip);

        public void DeleteCruiseShip(CruiseShip cruiseShip)
        {
            Delete(cruiseShip);
        }

        public void UpdateCruiseShip(CruiseShip cruiseShip)
        {
            Update(cruiseShip);
        }

        public async Task<IEnumerable<CruiseShip>> GetAllCruiseShipsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToListAsync();

        public async Task<CruiseShip> GetCruiseShipAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}

