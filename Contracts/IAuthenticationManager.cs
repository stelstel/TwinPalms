using Entities.DataTransferObjects;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAuthenticationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuth); 
        Task<string> CreateToken();
    }
}
