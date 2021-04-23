using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IOutletRepository
    {
        Task<IEnumerable<Outlet>> GetAllOutletsAsync(bool trackChanges);
        Task<Outlet> GetOutletAsync(int id, bool trackChanges);
        void CreateOutlet(Outlet outlet);
        void DeleteOutlet(Outlet outlet);
        void UpdateOutlet(Outlet outlet);
    }
}
