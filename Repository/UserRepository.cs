using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly RepositoryContext _repositoryContext;
        public UserRepository(RepositoryContext repositoryContext)
            :base(repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }


        public void DeleteUser(User user) => Delete(user);

        public void UpdateUser(User user) => Update(user);
        

        public async Task<User> GetUserAsync(string id, bool trackChanges) =>
            await FindByCondition(u => u.Id.Equals(id), trackChanges)
            .Include(u => u.OutletUsers).ThenInclude(ou => ou.Outlet)
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .SingleOrDefaultAsync();


        // TODO IEnumerable<> ska inte vara dynamic
        public async Task<IEnumerable<User>> GetUsersAsync(bool trackChanges)
        {
            /*return await (from user in _repositoryContext.Users
                          join userRoles in _repositoryContext.UserRoles on user.Id equals userRoles.UserId
                          join role in _repositoryContext.Roles on userRoles.RoleId equals role.Id

                          select new User
                          {
                              Id = user.Id,
                              UserName = user.UserName,
                              UserRoles = 

                          }).OrderBy(x => x.UserId).ToListAsync();*/

            return await FindAll(trackChanges)
                    .Include(u => u.OutletUsers).ThenInclude(ou => ou.Outlet)
                    .Include(u => u.HotelUsers).ThenInclude(hu => hu.Hotel)
                    //.Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                    .Select(u => new User
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        UserName = u.UserName,

                        Email = u.Email,
                        PhoneNumber = u.Email,
                        OutletUsers = u.OutletUsers/*.Select(ou => ou.Outlet)*/.ToList(),
                        HotelUsers = u.HotelUsers/*.Select(ou => ou.Outlet)*/.ToList()

                    })
                    .ToListAsync();
        }

        /*public void AddOutletsAsync(string[] outletIds, string userId, bool trackChanges)
        {
            throw new NotImplementedException();
            var user = GetUserAsync(userId, true);
           
        }*/
    }
}
