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
    class GuestSourceOfBusinessRepository : RepositoryBase<GuestSourceOfBusiness>, IGuestSourceOfBusinessRepository
    {
        public GuestSourceOfBusinessRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateGuestSourceOfBusiness(GuestSourceOfBusiness guestSourceOfBusiness) => Create(guestSourceOfBusiness);

        public void DeleteGuestSourceOfBusiness(GuestSourceOfBusiness guestSourceOfBusiness) => Delete(guestSourceOfBusiness);

        public void UpdateGuestSourceOfBusiness(GuestSourceOfBusiness guestSourceOfBusiness) => Update(guestSourceOfBusiness);

        public async Task<IEnumerable<GuestSourceOfBusiness>> GetAllGuestSourceOfBusinessAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.SourceOfBusiness)
            .ToListAsync();        

        public async Task<GuestSourceOfBusiness> GetGuestSourceOfBusinessAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();       
    }
}
