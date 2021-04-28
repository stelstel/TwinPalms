using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class CruiseCompanyRepository : RepositoryBase<CruiseCompany>, ICruiseCompanyRepository
    {
        public CruiseCompanyRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {

        }

        public void CreateCruiseCompany(CruiseCompany company) => Create(company);

        public void DeleteCruiseCompany(CruiseCompany company)
        {
            Delete(company);
        }

        public void UpdateCruiseCompany(CruiseCompany company)
        {
            Update(company);
        }

        public async Task<IEnumerable<CruiseCompany>> GetAllCruiseCompaniesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.Id)
            .ToListAsync();

        public async Task<CruiseCompany> GetCruiseCompanyAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}

