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
    public class OutletRepository : RepositoryBase<Outlet>, IOutletRepository
    {
        public OutletRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateOutlet(Outlet outlet) => Create(outlet);

        public void DeleteOutlet(Outlet outlet)
        {
            Delete(outlet);
        }

        public void UpdateOutlet(Outlet outlet)
        {
            Update(outlet);
        }

        public async Task<IEnumerable<Outlet>> GetAllOutletsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(o => o.Id)
            .ToListAsync();

        public async Task<Outlet> GetOutletAsync(int id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}

