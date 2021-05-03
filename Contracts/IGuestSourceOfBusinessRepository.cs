using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGuestSourceOfBusinessRepository
    {
        Task<IEnumerable<GuestSourceOfBusiness>> GetAllGuestSourceOfBusinessesAsync(bool trackChanges);
        Task<GuestSourceOfBusiness> GetGuestSourceOfBusinessAsync(int id, bool trackChanges);
        void CreateGuestSourceOfBusiness(GuestSourceOfBusiness guestSourceOfBusiness);
        void DeleteGuestSourceOfBusiness(GuestSourceOfBusiness guestSourceOfBusiness);
        void UpdateGuestSourceOfBusiness(GuestSourceOfBusiness guestSourceOfBusiness);
    }
}
