using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetAllHotelsAsync(bool trackChanges);
        Task<Hotel> GetHotelAsync(int id, bool trackChanges);
        void CreateHotel(Hotel hotel);
        void DeleteHotel(Hotel hotel);
        void UpdateHotel(Hotel hotel);
    }
}
