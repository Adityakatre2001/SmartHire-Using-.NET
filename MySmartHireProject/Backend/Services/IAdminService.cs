using SmartHire.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHire.Services
{
    public interface IAdminService
    {
        // User Management
        Task<UserDTO> CreateUserAsync(UserDTO userDTO);
        Task<UserDTO> UpdateUserAsync(long userId, UserDTO userDTO);
        Task DeleteUserAsync(long userId);
        Task<UserDTO> GetUserByIdAsync(long userId);
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();

        // Company Management
        Task<CompanyDTO> CreateCompanyAsync(CompanyDTO companyDTO);
        Task<CompanyDTO> UpdateCompanyAsync(long companyId, CompanyDTO companyDTO);
        Task DeleteCompanyAsync(long companyId);
        Task<CompanyDTO> GetCompanyByIdAsync(long companyId);
        Task<IEnumerable<CompanyDTO>> GetAllCompaniesAsync();
    }
}
