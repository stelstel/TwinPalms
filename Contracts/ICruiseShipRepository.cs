using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICruiseShipRepository
    {
        Task<IEnumerable<CruiseShip>> GetAllCruiseShipsAsync(bool trackChanges);
        Task<CruiseShip> GetCruiseShipAsync(int id, bool trackChanges);

        void CreateCruiseShip(CruiseShip cruiseShip);
        void DeleteCruiseShip(CruiseShip cruiseShip);
        void UpdateCruiseShip(CruiseShip cruiseShip);
    }
}
