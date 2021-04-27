using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace Contracts
{
    public interface IRoomTypeRepository
    {
        Task<IEnumerable<RoomType>> GetAllRoomTypesAsync(bool trackChanges);
        Task<RoomType> GetRoomTypeAsync(int id, bool trackChanges);

        void CreateRoomType(RoomType roomType);
        void DeleteRoomType(RoomType roomType);
        void UpdateRoomType(RoomType roomType);
    }
}
