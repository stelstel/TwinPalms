using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICruiseCompanyRepository
    {
        Task<IEnumerable<CruiseCompany>> GetAllCruiseCompaniesAsync(bool trackChanges);
        Task<CruiseCompany> GetCruiseCompanyAsync(int id, bool trackChanges);

        void CreateCruiseCompany(CruiseCompany cruiseCompany);
        void DeleteCruiseCompany(CruiseCompany cruiseCompany);
        void UpdateCruiseCompany(CruiseCompany cruiseCompany);
    }
}
