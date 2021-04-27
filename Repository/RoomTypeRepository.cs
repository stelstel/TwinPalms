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
    public class RoomTypeRepository : RepositoryBase<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateRoomType(RoomType roomType) => Create(roomType);

        public void DeleteRoomType(RoomType roomType) => Delete(roomType);

        public void UpdateRoomType(RoomType roomType) => Update(roomType);

        public async Task<IEnumerable<RoomType>> GetAllRoomTypesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

        public async Task<RoomType> GetRoomTypeAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}
