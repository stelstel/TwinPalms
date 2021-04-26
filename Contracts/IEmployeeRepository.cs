using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(int companyId, bool trackChanges);
        Task<Employee> GetEmployeeAsync(int companyId, int id, bool trackChanges);
        void CreateEmployeeForCompany(int companyId, Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);

    }
}
