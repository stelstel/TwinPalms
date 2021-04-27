using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ILocalEventRepository
    {
        Task<IEnumerable<LocalEvent>> GetAllLocalEventsAsync(bool trackChanges);
        Task<LocalEvent> GetLocalEventAsync(int id, bool trackChanges);

        void CreateLocalEvent(LocalEvent localEvent);
        void DeleteLocalEvent(LocalEvent localEvent);
        void UpdateLocalEvent(LocalEvent localEvent);
    }
}
