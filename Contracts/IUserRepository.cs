using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(string id, bool trackChanges);
        Task<User> GetUserAsync(string id, bool trackChanges, IList<string> userRoles);
        void AddUserConnectionsAsync(string id,  int[] outletIds, int[] hotelIds, bool trackChanges);
        void AddUserConnectionsAsync(string id,  int[] companyIds, bool trackChanges);
        Task<IEnumerable<int>> GetCompaniesAsync(string id, bool trackChanges);
        Task<IEnumerable<int>> GetOutletsAsync(string id, bool trackChanges);
       
        void DeleteUser(User user);
        void UpdateUser(User user);

    }
}
