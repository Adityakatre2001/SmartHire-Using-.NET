using System.Threading.Tasks;
using SmartHire.Models;

namespace SmartHire.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company> GetByIdAsync(long companyId); // Add this method
        Task<Company> CreateAsync(Company company);
        Task<Company> UpdateAsync(Company company);
        Task DeleteAsync(long companyId);
        Task<IEnumerable<Company>> GetAllAsync();
    }
}
