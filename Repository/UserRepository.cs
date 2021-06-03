﻿using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
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
            .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
            .Include(u => u.OutletUsers).ThenInclude(ou => ou.Outlet)
            .Include(u => u.HotelUsers).ThenInclude(hu => hu.Hotel)           
            .Include(u => u.CompanyUsers).ThenInclude(cu => cu.Company)
            .SingleOrDefaultAsync();

        public async Task<User> GetUserAsync(string id, bool trackChanges, IList<string> userRoles)
        {            
            if (!userRoles.Any(ur => ur.Contains("SuperAdmin")))        
            {
                if (userRoles.Any(ur => ur.Contains("Admin")))
                {
                    return await FindByCondition(u => u.Id.Equals(id), trackChanges)
                                      .Include(u => u.CompanyUsers).ThenInclude(cu => cu.Company)
                                      .SingleOrDefaultAsync();
                }
                else
                {
                    return await FindByCondition(u => u.Id.Equals(id), trackChanges)
                        .Include(u => u.OutletUsers).ThenInclude(ou => ou.Outlet)
                        .Include(u => u.HotelUsers).ThenInclude(hu => hu.Hotel)
                        .SingleOrDefaultAsync();
                }
            }           
            else return await FindByCondition(u => u.Id.Equals(id), trackChanges)                                    
                                    .SingleOrDefaultAsync();
        }

        /* private async Task<User> FindById(string id) =>
             await _repositoryContext.FindAsync(id);*/

        public async Task<IEnumerable<User>> GetUsersAsync(bool trackChanges)
        {
            /*return await (from user in _repositoryContext.Users
                          join userRoles in _repositoryContext.UserRoles on user.Id equals userRoles.UserId
                          join role in _repositoryContext.Roles on userRoles.RoleId equals role.Id
                          select new 
                          {
                              Id = user.Id,
                              FirstName = user.FirstName,
                              LastName = user.LastName,
                              UserName = user.UserName,
                              Email = user.Email,
                              Hotels = user.HotelUsers.Select(ur => ur.Hotel).ToList(),
                              Outlets = user.OutletUsers.Select(ur => ur.Outlet).ToList(),
                              Roles = user.UserRoles.Select(ur => ur.Role.Name).ToList()
                          }

                          ).OrderBy(x => x.Id).ToListAsync();*/

            return await FindAll(trackChanges)              
                    .Include(u => u.OutletUsers).ThenInclude(ou => ou.Outlet)
                    .Include(u => u.HotelUsers).ThenInclude(hu => hu.Hotel)
                    .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                    .Include(u => u.CompanyUsers).ThenInclude(cu => cu.Company)                    
                    .ToListAsync();
        }

        public async void AddUserConnectionsAsync(string id, int[] outletIds, int[] hotelIds, bool trackChanges)
        {
           var user = await FindByCondition(u => u.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
                    foreach (var hotelId in hotelIds)
                    {
                        user.HotelUsers.Add(new HotelUser { HotelId = hotelId, UserId = user.Id });
                    }
                    foreach (int outletId in outletIds)
                    {  
                        user.OutletUsers.Add(new OutletUser {OutletId = outletId, UserId = user.Id});
                    }
                await _repositoryContext.SaveChangesAsync();
        }

        public async Task<int> AddUserConnectionsAsync(string id, int[] companies, bool trackChanges)
        {
            var user = await FindByCondition(u => u.Id.Equals(id), trackChanges).FirstOrDefaultAsync();

            foreach (int companyId in companies)
            {
               
                user.CompanyUsers.Add(new CompanyUser { CompanyId = companyId, UserId = user.Id });

            }
            return await _repositoryContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<int>> GetCompaniesAsync(string id, bool trackChanges)
        {
            var companies = await FindByCondition(u => u.Id.Equals(id), trackChanges)
                .Select(user =>user.CompanyUsers.Select(c => c.CompanyId).ToArray())
                .FirstOrDefaultAsync();
            return companies;
        }

        public async Task<IEnumerable<int>> GetOutletsAsync(string id, bool trackChanges)
        {
            var outlets = await FindByCondition(u => u.Id.Equals(id), trackChanges)
                .Select(user => user.OutletUsers.Select(c => c.OutletId).ToArray())
                .FirstOrDefaultAsync();
            return outlets;
        }
    }
}
