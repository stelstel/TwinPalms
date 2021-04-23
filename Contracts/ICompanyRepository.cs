using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
        Task<Company> GetCompanyAsync(int id, bool trackChanges);

        void CreateCompany(Company company);
        void DeleteCompany(Company company);
        void UpdateCompany(Company company);
    }
}
