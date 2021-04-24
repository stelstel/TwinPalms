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
    public class HotelRepository : RepositoryBase<Hotel>, IHotelRepository
    {
        public HotelRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {

        }

        public void CreateHotel(Hotel hotel) => Create(hotel);

        public void DeleteHotel(Hotel hotel)
        {
            Delete(hotel);
        }

        public void UpdateHotel(Hotel hotel)
        {
            Update(hotel);
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(o => o.Name)
            .ToListAsync();

        public async Task<Hotel> GetHotelAsync(int id, bool trackChanges) =>
            await FindByCondition(o => o.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}

