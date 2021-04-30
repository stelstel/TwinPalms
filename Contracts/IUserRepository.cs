﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository
    {
        Task<IEnumerable<dynamic>> GetUsersAsync(bool trackChanges);
        Task<User> GetUserAsync(string id, bool trackChanges);
        //void AddOutletsAsync(string[] outletIds, bool trackChanges);
       
        void DeleteUser(User user);
        void UpdateUser(User user);

    }
}
