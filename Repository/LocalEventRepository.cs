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
    public class LocalEventRepository : RepositoryBase<LocalEvent>, ILocalEventRepository
    {
        public LocalEventRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateLocalEvent(LocalEvent localEvent) => Create(localEvent);

        public void DeleteLocalEvent(LocalEvent localEvent)
        {
            Delete(localEvent);
        }

        public void UpdateLocalEvent(LocalEvent localEvent)
        {
            Update(localEvent);
        }

        public async Task<IEnumerable<LocalEvent>> GetAllLocalEventsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(l => l.Event)
            .ToListAsync();

        public async Task<LocalEvent> GetLocalEventAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}

